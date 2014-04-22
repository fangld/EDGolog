(define (problem bomb-3-2)
   (:domain bomb)
   (:hostid a1)
   (:agents a1)
   (:objects  
       bomb1  bomb2  bomb3 - bomb 
	   
       toilet1  toilet2  - toilet)
	   
   (:init (or 
      (armed bomb1)
      (armed bomb2)
      (armed bomb3)
   ))
)
