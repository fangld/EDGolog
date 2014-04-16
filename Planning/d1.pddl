
(define (domain bomb)
    (:types bomb toilet)
    
(:predicates 
  (armed ?x - bomb)
  (clogged ?x - toilet)
)

(:action dunk
:parameters  (?bomb - bomb ?toilet - toilet)
:precondition (not (clogged ?toilet))
:effect
(and
(when (armed ?bomb) (not (armed ?bomb)))
(clogged ?toilet)))

(:action flush
:parameters  (?toilet - toilet)
:effect (when (clogged ?toilet) (not (clogged ?toilet))))

)

