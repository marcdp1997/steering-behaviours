### Implemented steering:                                                                                                                                    
Arrive, seek, flee, pursuit, evade, wander, obstacle avoidance, separation between agents and queue.                                                

### How it works:                                              
In this project there are 3 scenes for now:
1) **HunterPrey**: Various hunters are trying to catch some preys. If they get too close, preys will evade them.                                       
   _Steerings used: wander, pursuit, evade and separation._
2) **CustomerQueue**: Watch some customers try to go through a small corridor. Move the camera with WASD.                          
   _Steerings used: arrive, separation, avoidance and queue._
3) **PursuitWithObstacles**: Move the central square with WASD, it will be the target for these pursuers.                            
   _Steerings used: pursuit, separation and avoidance._

Github repository: https://github.com/marcdp1997/steering-behaviours                                         
Author: Marc de Pedro.
