(seq 
    (while (not (know (loc a1 3)))
        (seq 
	        (if (forall (?n - numOfAcorn) (not (bel (exists (?x - point) (and (loc a1 ?x) (acorn ?x ?n))))))
	            (Smell a1)
			)
			
		    (if (and (bel (exists (?x - point) (and (loc a1 ?x) (exists (?n - numOfAcorn) (and (loc a1 ?x) (acorn ?x ?n) (> ?n 0))))))
					(know (not (hold a1)))
				)
		        (Pick a1)
			)
		    
			(Right a1)
	    )
    )
	
	(Drop a1)

	(Noop a1)
	(Noop a1)
	(Noop a1)
)
