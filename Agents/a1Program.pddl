(seq 
    (while (know (not (loc a1 3)))
        (seq 
	        (if (not (know (hold a1)))
	            (pick a1)
			)
		    
			(right a1)
	    )
    )
	
	(if (and (know (hold a1)) (bel (not (acorn 3 3))))
		(drop a1)
	)
	(empty a1)
	(empty a1)
	(empty a1)
)