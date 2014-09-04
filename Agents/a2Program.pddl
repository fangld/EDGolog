(seq 
    (while (know (not (loc a2 0)))
        (seq 
	        (if (forall (?n - numOfAcorn) (not (bel (exists (?x - point) (and (loc a2 ?x) (acorn ?x ?n))))))
	            (smell a2)
			)
			
		    (if (and (bel (exists (?x - point) (and (loc a2 ?x) (exists (?n - numOfAcorn) (and (loc a2 ?x) (acorn ?x ?n) (> ?n 0))))))
					(know (not (hold a2)))
				)
		        (pick a2)
			)
		    
			(left a2)
	    )
    )
	
	(drop a2)
	(empty a2)
	(empty a2)
	(empty a2)
)
