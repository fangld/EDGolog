(seq 
    (while (not (know (loc a2 0)))
        (seq
	        (if (know (not (hold a2)))
	            (Pick a2)
			)
		    
			(Left a2)
	    )
    )
	
	(if (and (know (hold a2)) (bel (not (acorn 3 0))))
		(Drop a2)
	)

	(Noop a2)
	(Noop a2)
	(Noop a2)
)