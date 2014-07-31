(seq 
    (while (know (not (loc a1 7)))
        (seq 
	        (if (not (exists (?x - point ?n - numOfAcorn) (bel (and (loc a1 ?x) (acorn ?x ?n)))))
	            (smell a1)
			)
			
		    (if (and 
			        (exists (?x - point ?n - numOfAcorn) (bel (and (loc a1 ?x) (acorn ?x ?n) (> ?n 0))))
					(know (not (hold a1)))
				)
		        (pick a1)
			)
		    
			(right a1)
	    )
    )
	
	(drop a1)
	(empty a1)
	(empty a1)
	(empty a1)
)