﻿(define (problem sw-7-3)
	(:domain squirrelsWorlds)
	(:agent a1 a2)
	(:agentid a2)
	(:numbers (maxLoc 7) (maxAcorn 3) (noticeRelLoc 1) (errorSencingAcorn 1))
	(:initknowledge (and 
      	(loc a1 0)
		(loc a2 7)
		(not (hold a1))
		(not (hold a2))
        )
	)
)