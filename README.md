
- [Main Questions](#main-questions)
- [Functional Explanation - TODO](#functional-explanation---todo)
- [Lore - TODO](#lore---todo)
- [Classes](#classes)
  - [Collective](#collective)
    - [Nation](#nation)
    - [Tribe](#tribe)
  - [Ruler](#ruler)
    - [Player](#player)
    - [AI](#ai)
  - [Being](#being)
    - [Champion](#champion)
    - [Creature](#creature)
      - [Monster](#monster)
      - [**Beast**](#beast)
- [Scene Tree](#scene-tree)
  - [CollectivesManager](#collectivesmanager)
  - [RulersManager](#rulersmanager)
  - [Game](#game)
- [List of Known Creatures](#list-of-known-creatures)
- [List of Known Places](#list-of-known-places)
- [Playable Races](#playable-races)
  - [Humans](#humans)
- [Buildings](#buildings)
  - [Human](#human)
    - [Keep](#keep)
    - [Training Grounds](#training-grounds)
    - [Farms](#farms)
- [Concepts](#concepts)
  - [Relation](#relation)
  - [Race](#race)
- [Other](#other)
  - [Node](#node)


# Main Questions  

- Is there **Procedural Generation ? YES**
    - implication is I need to be able to draw a map from the generated map. so tiles must be small, need fluid edges.


Optional Continent names : Otra, Roshax, Athia  - one of these chosen at random.

---
# Functional Explanation - TODO  

- You are the ruler of a *Kingdom* (chosen race and political structure at start of new game).
- There are some number of kindgoms on the continent (Ideally 8-10).
- Each *Kingdom* will have a relationship with every other *Kingdom* (this could be just knowledge of them, or could be a alliance
  
---

# Lore - TODO  


Lore is variable. Every time the world loads new lore gets generated. This will be a large task.

The storm dragon Xazhi/Saeclus and Fire dragon Unua/Libitus live on either end of the continent. Many older religions worship them as dieties

The forest of Vilis is home to some dark and dangerous creatures (see [**creatures list**](#list-of-known-creatures) )

---
# Classes

## Collective
**Base class** for groups of actors that engage with other groups of actors. These are nations or tribes.  Collectives have relationships with other collectives that they know about. Collectives exist as objects handled by the [**CollectivesManager**](#collectivesmanager)

### Nation  
Inherits : [**Collective**](#collective)  

Nations are playable Kingdoms. 

### Tribe
Inherits : [**Collective**](#collective)  

---
## Ruler
*Inherits : [**Node**](#node)*   
Actors are the players of the game, these can be a player or an AI.
They take control of a Collective and make its decisions. (Actors are the rulers of colletives)

### Player
*Inherits : [**Ruler**](#ruler)*   
The Player.

### AI
*Inherits : [**Ruler**](#ruler)*   
Enemies are all Rulers other than the player.
 
## Being 
**Base class** for all living things with some intelligence. This includes plants-creatures that have intelligence, but not things like trees. Champions, Leaders, Wolves etc. they have the ability to affect other beings and terrain.

### Champion  
*Inherits : [**Being**](#being)*  
Champions are induviduals belonging to an intelligent race who exist in the world. They have allegience to certain collectives but make decisions based on their own interests.

### Creature
*Inherits : [**Being**](#being)*  
Creatures covers any race that is non playable. This includes a range of intelligence (goblins, dragons), the key feature is that creatures do not form [**nations**](#nation) (they may form [**tribes**](#tribe)). Creatures are further broken down into two main classes, [**Monsters**](#monster) and [**Beasts**](#beast)

#### Monster   
*Inherits : [Creature](#creature)*    
[**Monster**](#monster) is a high level creature. These are rare compared to [**Beasts**](#beast), not hunted for food or used in regular life. Their presence is usually treated as a large scale threat. Any [**Monster**](#monster) is able to destroy a village easily, but only a select few could take on a capital city alone, based ont their hazard class.

#### **Beast**  
*Inherits : [**Creature**](#creature)*   
Beast is a low level creature. These are usually native inhabitants of an area, can be domesticated by inhabitants of a region.

# Scene Tree 

## CollectivesManager  
*Inherits : [**Node**](#node)*   
Manages the collectives. This is the node that holds the collectives as objects. 

## RulersManager  
*Inherits : [**Node**](#node)*   
Manages the rulers. 

## Game  
Handles communication between top level nodes in the scene tree. Rulers, camera, map etc..


# List of Known Creatures

# List of Known Places

# Playable Races
  ## Humans

# Buildings
## Human
  ### Keep
  ### Training Grounds
  ### Farms
  ###

# Concepts  

## Relation  
Property of [Collective](#collective)  
Descibes how Rulers view each other, if there is no relation, they are not aware of each others presence.


## Race
Property of [Being](#being)  
Race determines a series of traits



# Other

## Node  
Godot Node

