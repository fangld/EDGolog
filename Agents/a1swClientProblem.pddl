﻿(define (problem sw-7-3)
	(:domain squirrelsWorlds)
	(:agent a1 a2)
	(:agentid a1)
	(:numbers (maxLoc 7) (maxAcorn 3) (noticeRelLoc 1) (errorSencingAcorn 1))
	(:initknowledge (and 
      	(loc a1 0)
		(not (loc a1 1))
		(not (loc a1 2))
		(not (loc a1 3))
		(not (loc a1 4))
		(not (loc a1 5))
		(not (loc a1 6))
		(not (loc a1 7))
		(not (loc a2 0))
		(not (loc a2 1))
		(not (loc a2 2))
		(not (loc a2 3))
		(not (loc a2 4))
		(not (loc a2 5))
		(not (loc a2 6))
		(loc a2 7)
		(not (hold a1))
		(not (hold a2))
        )
	)
)