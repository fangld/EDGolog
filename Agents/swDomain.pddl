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
	(loc ?i - agent ?x - point oneof ?x)
	(acorn ?x - point ?n - numOfAcorn oneof ?n)
)

(:event leftSuc2
:parameters (?i - agent ?d - leftRelLoc)
:precondition (exists (?x - point ?y - point ?j - agent)
                      (and (!= ?i ?j) (loc ?i ?x) (loc ?j ?y) (= ?x (+ ?y ?d)) (!= ?x 0))
			  )
:effect (forall (?x - point)
               (when (and (loc ?i ?x) (!= ?x 0)) (and (loc ?i (- ?x 1)) (not (loc ?i ?x))))
		)
)

(:event leftSuc1
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

(:event rightSuc2
:parameters (?i - agent ?d - rightRelLoc)
:precondition (exists (?x - point ?y - point ?j - agent)
                      (and (!= ?i ?j) (loc ?i ?x) (loc ?j ?y) (= ?x (+ ?y ?d)) (!= ?x maxLoc))
			  )
:effect (forall (?x - point)
               (when (and (loc ?i ?x) (!= ?x maxLoc)) (and (loc ?i (+ ?x 1)) (not (loc ?i ?x))))
		)
)

(:event rightSuc1
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
:precondition (exists (?x - point  ?n - numOfAcorn) (and (acorn ?x ?n) (loc ?i ?x)  (hold ?i) (< ?n maxAcorn)))
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

(:event noop
:parameters (?i - agent)
)


(:action Left
:parameters (?i - agent)

(:response LeftSuc2
:parameters (?d - leftRelLoc)
:eventmodel (leftSuc2 ?i ?d))

(:response LeftSuc1
:eventmodel (leftSuc1 ?i))

(:response LeftFail
:eventmodel (leftFail ?i))
)


(:action Right
:parameters (?i - agent)

(:response RightSuc2
:parameters (?d - rightRelLoc)
:eventmodel (rightSuc2 ?i ?d))

(:response RightSuc1
:eventmodel (rightSuc1 ?i))

(:response RightFail
:eventmodel (rightFail ?i))
)


(:action Pick
:parameters (?i - agent)

(:response PickSuc
:eventmodel (pickSuc ?i))

(:response PickFail
:eventmodel (pickFail ?i))
)


(:action Drop
:parameters (?i - agent)

(:response DropSuc
:eventmodel (dropSuc ?i))

(:response DropFail
:eventmodel (dropFail ?i))
)


(:action Smell
:parameters (?i - agent)

(:response Learn
:parameters (?m - numOfAcorn)
:eventmodel (
	        (:pldegree 0
             :events (learn ?i ?m 0))
            (:pldegree 1 
			 :events (exists (?d - noiseSensingAcorn) (and (!= ?d 0) (learn ?i ?m ?d))))
		    )
))


(:action Noop
:parameters (?i - agent)

(:response Noop
:eventmodel (noop ?i))
)



(:observation ObsLeftSuc
:parameters (?i - agent ?j - agent ?d - leftRelLoc)
:precondition (exists (?x - point ?y - point) (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (= ?x (+ ?y ?d))))
:eventmodel (leftSuc2 ?j ?d)
)

(:observation ObsRightSuc
:parameters (?i - agent ?j - agent ?d - rightRelLoc)
:precondition (exists (?x - point ?y - point) (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (= ?x (+ ?y ?d))))
:eventmodel (rightSuc2 ?j ?d)
)

(:observation ObsLeftFail
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) 
                      (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (or (= ?x (+ ?y 1)) (= ?x ?y))))
:eventmodel (leftFail ?j)
)

(:observation ObsRightFail
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) 
                      (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (or (= ?x (- ?y 1)) (= ?x ?y))))
:eventmodel (rightFail ?j)
)

(:observation ObsPickSuc
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) 
                      (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (or (= ?x (- ?y 1)) (= ?x ?y))))
:eventmodel (pickSuc ?j)
)

(:observation ObsDropSuc
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) 
                      (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (or (= ?x (- ?y 1)) (= ?x ?y))))
:eventmodel (dropSuc ?j)
)

(:observation ObsPickFail
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) 
                      (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (or (= ?x (- ?y 1)) (= ?x ?y))))
:eventmodel (pickFail ?j)
)

(:observation ObsDropFail
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) 
                      (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (or (= ?x (- ?y 1)) (= ?x ?y))))
:eventmodel (dropFail ?j)
)

(:observation ObsSmell
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) 
                      (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (or (= ?x (+ ?y 1)) (= ?x (- ?y 1)) (= ?x ?y))))
:eventmodel (exists (?m - numOfAcorn ?d - noiseSensingAcorn) (learn ?j ?m ?d))
)

(:observation ObsNoop
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point) 
                      (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (or (= ?x (+ ?y 1)) (= ?x (- ?y 1)) (= ?x ?y))))
:eventmodel (noop ?j)
)

(:observation NoObs
:parameters (?i - agent ?j - agent)
:precondition (exists (?x - point ?y - point)
                      (and (!= ?i ?j) (loc ?j ?x) (loc ?i ?y) (!= ?x (+ ?y 1)) (!= ?x (- ?y 1)) (!= ?x ?y))
					  )
:eventmodel (
	        (:pldegree 0
             :events (noop ?j))
            (:pldegree 1 
			 :events (or (leftSuc1 ?j) (rightSuc1 ?j) (leftFail ?j) (rightFail ?j) 
			             (pickSuc ?j) (dropSuc ?j) (pickFail ?j) (dropFail ?j)
	                     (exists (?m - numOfAcorn ?d - noiseSensingAcorn) (learn ?j ?m ?d))
	                 )
			)
		    )
)
)