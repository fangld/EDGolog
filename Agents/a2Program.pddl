(seq 
    (while (know (not (loc a2 0)))
        (seq 
	        (if (not (exists (?x - point ?n - numOfAcorn) (bel (and (loc a2 ?x) (acorn ?x ?n)))))
	            (smell a2)
			)
			
		    (if (and 
			        (exists (?x - point ?n - numOfAcorn) (bel (and (loc a2 ?x) (acorn ?x ?n) (> ?n 0))))
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
