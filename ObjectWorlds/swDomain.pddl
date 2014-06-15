(define (domain squirrelsWorlds)
	(:constants maxLoc maxAcorn noticeRelLoc errorSencingAcorn)
	(:types (numOfAcorn 0 maxAcorn)
	        (point 0 maxLoc)
	        (leftRelLoc (- noticeRelLoc) (+ noticeRelLoc 1))
			(rightRelLoc (- (- noticeRelLoc) 1) noticeRelLoc)
			(noiseSensingAcorn (- errorSencingAcorn) errorSencingAcorn)
			)
    
(:predicates
	(hold ?i - agent)
	(loc ?i - agent ?x - point)
	(acorn ?x - point ?n - numOfAcorn)
)

(:event leftSucWithNotice
:parameters (?i - agent ?d - leftRelLoc)
:precondition (exists (?x - point ?y - point ?j - agent) 
                      (and (not (= ?i ?j)) (loc ?i ?x) (loc ?j ?y) (= ?x (+ ?y ?d))))
:effect (forall (?x - point)
               (when (and (loc ?i ?x) (not (= ?x 0))) (and (loc ?i (- ?x 1)) (not (loc ?i ?x))))
		)
)

(:event leftSucWithoutNotice
:parameters (?i - agent)
:precondition (exists (?x - point ?y - point ?j - agent) 
                      (and (not (= ?i ?j)) (loc ?i ?x) (loc ?j ?y) (forall (?r - leftRelLoc) (not  (= ?x (+ ?y ?r)))))
			  )
:effect (forall (?x - point)
                (when (and (loc ?i ?x) (not (= ?x 0))) (and (loc ?i (- ?x 1)) (not (loc ?i ?x))))
		)
)

(:event leftFail
:parameters (?i - agent)
:precondition (exists (?x - point) (and (loc ?i ?x) (= ?x 0)))
)

(:event rightSucWithNotice
:parameters (?i - agent ?d - rightRelLoc)
:precondition (exists (?x - point ?y - point) (and (loc ?i ?x) (loc j ?y) (= ?x (+ ?y ?d))))
:effect (forall (?x - point) 
               (when (and (loc ?i ?x) (not (= ?x maxLoc))) (and (loc ?i (+ ?x 1)) (not (loc ?i ?x))))
		)
)

(:event rightSucWithoutNotice
:parameters (?i - agent)
:precondition (exists (?x - point ?y - point ?j - agent) 
                      (and (not (= ?i ?j)) (loc ?i ?x) (loc ?j ?y) (forall (?r - rightRelLoc) (not  (= ?x (+ ?y ?r)))))
			  )			   
:effect (forall (?x - point)
               (when (and (loc ?i ?x) (not (= ?x 0))) (and (loc ?i (- ?x 1)) (not (loc ?i ?x))))
		)
)

(:event rightFail
:parameters (?i - agent)
:precondition (exists (?x - point) (and (loc ?i ?x) (= ?x maxLoc)))
)

(:event pickSuc
:parameters (?i - agent)
:precondition (exists (?x - point  ?n - numOfAcorn) (and (acorn ?x ?n) (loc ?i ?x) (not (hold ?i))))
:effect (forall (?x - point ?n - numOfAcorn)
                (when (and (loc ?i ?x) (acorn ?x ?n) (not (= ?n 0))) (and (acorn ?x (- ?n 1)) (not (acorn ?x ?n)) (hold ?i))))
)

(:event pickFail
:parameters (?i - agent)
:precondition (or (hold ?i) (exists (?x - point) (and (loc ?i ?x) (acorn ?x 0))))
)

(:event dropSuc
:parameters (?i - agent)
:precondition (exists (?x - point  ?n - numOfAcorn) (and (acorn ?x ?n) (loc ?i ?x) (not (hold ?i))))
:effect (forall (?x - point ?n - numOfAcorn) 
                (when (and (loc ?i ?x) (acorn ?x ?n) (not (= ?n maxAcorn))) (and (acorn ?x (+ ?n 1)) (not (acorn ?x ?n)) (not (hold ?i)))))
)

(:event dropFail
:parameters (?i - agent)
:precondition (or (not (hold ?i)) (exists (?x - point) (and (loc ?i ?x) (acorn ?x maxAcorn))))
)

(:event learn
:parameters (?i - agent ?m - numOfAcorn ?d - diffSensing)
:precondition (exists (?x - point ?n - numOfAcorn) (and (acorn ?x ?n) (loc ?i ?x) (= ?n (+ ?m ?d))))
)

(:event nil
:parameters (?i - agent)
)



(:action left 
(:response sucWithNotice
:parameters (?d - leftRelLoc)
:events (leftSucWithNotice self ?d))

(:response sucWithoutNotice
:events (leftSucWithoutNotice self))

(:response fail
:events (leftFail self))
)



(:action right 
(:response sucWithNotice
:parameters (?d - rightRelLoc)
:events (rightSucWithNotice self ?d))

(:response sucWithoutNotice
:events (rightSucWithoutNotice self))

(:response fail
:events (leftFail self))
)



(:action pick
(:response suc
:events (pickSuc self))

(:response fail
:events (pickFail self) 
))



(:action drop
(:response suc
:events (dropSuc self))
(:response fail
:events (dropFail self) 
))


(:action smell
(:response noise
:parameters (?m - numOfAcorn)
:events (exists (?d - noiseSensingAcorn) (learn self ?m ?d))
))



(:action empty 
(:response suc
:events (nil self))
)



(:observation otherLeftSuc
:parameters (?d - leftRelLoc)
:precondition (exists (?x - point ?y - point) (and (loc other ?x) (loc self ?y) (= ?x (+ ?y ?d))))
:events (leftSucWithNotice other ?d)
)

(:observation otherRightSuc
:parameters (?d - rightRelLoc)
:precondition (exists (?x - point ?y - point) (and (loc other ?x) (loc self ?y) (= ?x (+ ?y ?d))))
:events (rightSucWithNotice other ?d)
)

(:observation otherLeftFail
:precondition (exists (?x - point ?y - point) (and (loc other ?x) (loc self ?y) (or (= ?x (+ ?y 1)) (= ?x ?y))))
:events (leftFail other)
)

(:observation otherRightFail
:precondition (exists (?x - point ?y - point) (and (loc other ?x) (loc self ?y) (or (= ?x (- ?y 1)) (= ?x ?y))))
:events (rightFail other)
)

(:observation otherPickSuc
:precondition (exists (?x - point ?y - point) (and (loc other ?x) (loc self ?y) (= ?x ?y)))
:events (pickSuc other)
)

(:observation otherDropSuc
:precondition (exists (?x - point ?y - point) (and (loc other ?x) (loc self ?y) (= ?x ?y)))
:events (dropSuc other)
)

(:observation otherPickFail
:precondition (exists (?x - point ?y - point) (and (loc other ?x) (loc self ?y) (= ?x ?y)))
:events (pickFail other)
)

(:observation otherDropFail
:precondition (exists (?x - point ?y - point) (and (loc other ?x) (loc self ?y) (= ?x ?y)))
:events (dropFail other)
)

(:observation otherPickSucOrFail
:precondition (exists (?x - point ?y - point) (and (loc other ?x) (loc self ?y) (or (= ?x (+ ?y 1)) (= ?x (- ?y 1)))))
:events ((0 (pickSuc other)) (1 (pickFail other)))
)

(:observation otherDropSucOrFail
:precondition (exists (?x - point ?y - point) (and (loc other ?x) (loc self ?y) (or (= ?x (+ ?y 1)) (= ?x (- ?y 1)))))
:events ((0 (dropSuc other)) (1 (dropFail other)))
)

(:observation otherSmell
:precondition (exists (?x - point ?y - point) (and (loc other ?x) (loc self ?y) (or (= ?x (+ ?y 1)) (= ?x (- ?y 1)) (= ?x ?y))))
:events (exists (?m - numOfAcorn ?d - noiseSensingAcorn) (learn other ?m ?d))
)

(:observation noticeEmtpy
:precondition (exists (?x - point ?y - point) (and (loc other ?x) (loc self ?y) (or (= ?x (+ ?y 1)) (= ?x (- ?y 1)) (= ?x ?y))))
:events (nil other)
)

(:observation noinfo
:precondition (exists (?x - point ?y - point) (and (loc other ?x) (loc self ?y) (not (= xi (+ xj 1))) (not (= xi (- xj 1))) (not (= xi xj))))
:events 
((0 (nil other))
(1 (or (leftSucWithoutNotice other) (rightSucWithoutNotice other)
	   (leftFail other) (rightFail other) (pickSuc other) (dropSuc other) (pickFail other) (dropFail other)
	   (exists (?m - numOfAcorn ?d - noiseSensingAcorn) (learn other ?m ?d))
	   ))
	   )
)
)