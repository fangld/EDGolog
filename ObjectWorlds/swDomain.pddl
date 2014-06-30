(define (domain squirrelsWorlds)
	(:numbers maxLoc maxAcorn noticeRelLoc errorSencingAcorn)
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
                      (and (!= ?i ?j) (loc ?i ?x) (loc ?j ?y) (= ?x (+ ?y ?d)) (!= ?x 0))
			  )
:effect (forall (?x - point)
               (when (and (loc ?i ?x) (!= ?x 0)) (and (loc ?i (- ?x 1)) (not (loc ?i ?x))))
		)
)

(:event leftSucWithoutNotice
:parameters (?i - agent)
:precondition (exists (?x - point ?y - point ?j - agent)
                      (and (!= ?i ?j) (loc ?i ?x) (loc ?j ?y) (!= ?x 0) (forall (?r - leftRelLoc) (!= ?x (+ ?y ?r))))
			  )
:effect (forall (?x - point)
                (when (and (loc ?i ?x) (!= ?x 0)) (and (loc ?i (- ?x 1)) (not (loc ?i ?x))))
		)
)

(:event leftFail
:parameters (?i - agent)
:precondition (exists (?x - point) (and (loc ?i ?x) (= ?x 0)))
)

(:event rightSucWithNotice
:parameters (?i - agent ?d - rightRelLoc)
:precondition (exists (?x - point ?y - point ?j - agent)
                      (and (!= ?i ?j) (loc ?i ?x) (loc ?j ?y) (= ?x (+ ?y ?d)) (!= ?x maxLoc))
			  )
:effect (forall (?x - point)
               (when (and (loc ?i ?x) (!= ?x maxLoc)) (and (loc ?i (+ ?x 1)) (not (loc ?i ?x))))
		)
)

(:event rightSucWithoutNotice
:parameters (?i - agent)
:precondition (exists (?x - point ?y - point ?j - agent)
                      (and (!= ?i ?j) (loc ?i ?x) (loc ?j ?y) (!= ?x maxLoc) (forall (?r - rightRelLoc) (!= ?x (+ ?y ?r))))
			  )
:effect (forall (?x - point)
               (when (and (loc ?i ?x) (!= ?x maxLoc)) (and (loc ?i (+ ?x 1)) (not (loc ?i ?x))))
		)
)

(:event rightFail
:parameters (?i - agent)
:precondition (exists (?x - point) (and (loc ?i ?x) (= ?x maxLoc)))
)

(:event pickSuc
:parameters (?i - agent)
:precondition (exists (?x - point ?n - numOfAcorn) (and (acorn ?x ?n) (loc ?i ?x) (not (hold ?i)) (> ?n 0)))
:effect (forall (?x - point ?n - numOfAcorn)
                (when (and (loc ?i ?x) (acorn ?x ?n) (!= ?n 0)) (and (acorn ?x (- ?n 1)) (not (acorn ?x ?n)) (hold ?i))))
)

(:event pickFail
:parameters (?i - agent)
:precondition (or (hold ?i) (exists (?x - point) (and (loc ?i ?x) (acorn ?x 0))))
)

(:event dropSuc
:parameters (?i - agent)
:precondition (exists (?x - point  ?n - numOfAcorn) (and (acorn ?x ?n) (loc ?i ?x) (not (hold ?i)) (< ?n maxAcorn)))
:effect (forall (?x - point ?n - numOfAcorn)
                (when (and (loc ?i ?x) (acorn ?x ?n) (!= ?n maxAcorn)) (and (acorn ?x (+ ?n 1)) (not (acorn ?x ?n)) (not (hold ?i)))))
)

(:event dropFail
:parameters (?i - agent)
:precondition (or (not (hold ?i)) (exists (?x - point) (and (loc ?i ?x) (acorn ?x maxAcorn))))
)

(:event learn
:parameters (?i - agent ?m - numOfAcorn ?d - noiseSensingAcorn)
:precondition (exists (?x - point ?n - numOfAcorn) (and (acorn ?x ?n) (loc ?i ?x) (= ?n (+ ?m ?d))))
)

(:event nil
:parameters (?i - agent)
)




(:action left
:parameters (?i - agent)

(:response sucWithNotice
:parameters (?d - leftRelLoc)
:events (leftSucWithNotice ?i ?d))

(:response sucWithoutNotice
:events (leftSucWithoutNotice ?i))

(:response fail
:events (leftFail ?i))
)



(:action right
:parameters (?i - agent)

(:response sucWithNotice
:parameters (?d - rightRelLoc)
:events (rightSucWithNotice ?i ?d))

(:response sucWithoutNotice
:events (rightSucWithoutNotice ?i))

(:response fail
:events (leftFail ?i))
)



(:action pick
:parameters (?i - agent)

(:response suc
:events (pickSuc ?i))

(:response fail
:events (pickFail ?i))
)



(:action drop
:parameters (?i - agent)

(:response suc
:events (dropSuc ?i))

(:response fail
:events (dropFail ?i))
)


(:action smell
:parameters (?i - agent)

(:response noise
:parameters (?m - numOfAcorn)
:events ((0 (learn ?i ?m 0))
         (1 (exists (?d - noiseSensingAcorn) (and (!= ?d 0) (learn ?i ?m ?d))))
		)
)
)



(:action empty
:parameters (?i - agent)

(:response suc
:events (nil ?i))
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
:events ((0 (pickSuc other)) 
         (1 (pickFail other))
		)
)

(:observation otherDropSucOrFail
:precondition (exists (?x - point ?y - point) (and (loc other ?x) (loc self ?y) (or (= ?x (+ ?y 1)) (= ?x (- ?y 1)))))
:events ((0 (dropSuc other)) 
         (1 (dropFail other)))
		)
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
:precondition (exists (?x - point ?y - point) 
                      (and (loc other ?x) (loc self ?y) (!= xi (+ xj 1)) (!= xi (- xj 1)) (!= xi xj))
					  )
:events 
((0 (nil other))
(1 (or (leftSucWithoutNotice other) (rightSucWithoutNotice other)
	   (leftFail other) (rightFail other) (pickSuc other) (dropSuc other) (pickFail other) (dropFail other)
	   (exists (?m - numOfAcorn ?d - noiseSensingAcorn) (learn other ?m ?d))
	   ))
	   )
)
)