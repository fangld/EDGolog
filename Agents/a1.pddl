(define (problem bomb-3-2)
   (:domain bomb)
   (:agents a1)
   (:agentid a1)
   (:objects  
       bomb1  bomb2  bomb3 - bomb 
	   
       toilet1  toilet2  - toilet)
	   
   (:initknowledge (or 
      (armed bomb1)
      (armed bomb2)
      (armed bomb3)
   ))
)
