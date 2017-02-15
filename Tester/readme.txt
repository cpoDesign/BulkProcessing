Example how to use Akka.NET

System is composed from multiple actors that get dynamically generated and torn down when they are not used.

System is dynamically scalable

======================================
==Error handling ==
======================================

Actor supervises its own children

Actor can throw exception the actor it self and all of its children become suspended from processing
Next the failure gets propagated to parent, who decides what to do and respons to its child what to do.
It can say 
    - restart
        - all messages in inbox are preserved
        - the decision goes down through its hierarchy
        - if exception happened the exception gets ignored
        - the child state is maitained
        - all children are resumed
         - if child cannot be recovered, the child state will get lost
           - as consequence of the restart all the children are restarted also (default behavior)
    - resume processing
    - Stop permanently - move it to terminated state
        - terminates all children too
    - escalate to its parent (can goes multiple levels)


======================================
== Supervision strategies ==
======================================
We can define optional number of exceptions we allow in time period, it this exceed the child will be stopped.

1.OneForOneStrategy = Default strategy
If there is only one actor failing, the strategy will be applied only for the one actor

Example:
if there Stop error handling, only the one actor will get stopped.

2. AllForOneStrategy
If one of the children has failed, the error handling policy will get applied to all children actors

- used when the child actors depend of itself and therefore they cannot do their work

























======================================
==Nuget packages==
======================================

Akka.Persistance - saves state data to database, if required


