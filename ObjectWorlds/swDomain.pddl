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
:parameters (?i - agent ?j - agent ?d - leftRelLoc)
:precondition (exists (?x - point ?y - point) (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (= ?x (+ ?y ?d))))
:events (leftSucWithNotice ?j ?d)
)

(:observation otherRightSuc
:parameters (?i - agent ?j - agent ?d - rightRelLoc)
:precondition (exists (?x - point ?y - point) (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (= ?x (+ ?y ?d))))
:events (rightSucWithNotice ?j ?d)
)

(:observation otherLeftFail
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (or (= ?x (+ ?y 1)) (= ?x ?y))))
:events (leftFail ?j)
)

(:observation otherRightFail
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (or (= ?x (- ?y 1)) (= ?x ?y))))
:events (rightFail ?j)
)

(:observation otherPickSuc
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (= ?x ?y)))
:events (pickSuc ?j)
)

(:observation otherDropSuc
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (= ?x ?y)))
:events (dropSuc ?j)
)

(:observation otherPickFail
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (= ?x ?y)))
:events (pickFail ?j)
)

(:observation otherDropFail
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (= ?x ?y)))
:events (dropFail ?j)
)

(:observation otherPickSucOrFail
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (or (= ?x (+ ?y 1)) (= ?x (- ?y 1)))))
:events ((0 (pickSuc ?j)) 
         (1 (pickFail ?j))
		)
)

(:observation otherDropSucOrFail
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (or (= ?x (+ ?y 1)) (= ?x (- ?y 1)))))
:events ((0 (dropSuc ?j))
         (1 (dropFail ?j)))
		)
)

(:observation otherSmell
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (or (= ?x (+ ?y 1)) (= ?x (- ?y 1)) (= ?x ?y))))
:events (exists (?m - numOfAcorn ?d - noiseSensingAcorn) (learn ?j ?m ?d))
)

(:observation otherEmpty
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (or (= ?x (+ ?y 1)) (= ?x (- ?y 1)) (= ?x ?y))))
:events (nil ?j)
)

(:observation noinfo
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point)
                      (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (!= xi (+ xj 1)) (!= xi (- xj 1)) (!= xi xj))
					  )
:events 
((0 (nil ?j))
(1 (or (leftSucWithoutNotice ?j) (rightSucWithoutNotice ?j)
	   (leftFail ?j) (rightFail ?j) (pickSuc ?j) (dropSuc ?j) (pickFail ?j) (dropFail ?j)
	   (exists (?m - numOfAcorn ?d - noiseSensingAcorn) (learn ?j ?m ?d))
	   ))
	   )
)
)