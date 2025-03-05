# ProjetReservation

Cette API permet de gérer la réservation de salles de réunion. Les fonctionnalités incluent :
- Lister des salles
- Créer des réservations
- Supprimer des réservations
- Proposer des créneaux libres en cas de conflit de réservation

## Fonctionnalités

### Lister des salles
Permet de récupérer la liste des salles disponibles.

### Créer des réservations
Permet de créer une réservation pour une salle spécifique.

### Supprimer des réservations
Permet de supprimer une réservation existante.

### Gestion des conflits de réservation
En cas de conflit (de réservation), l’API propose tous les créneaux libres de la journée demandée.

## Détails des réservations
- **User** : Nom de la personne effectuant la réservation.
- **Room** : Nom ou ID de la salle réservée.
- **Créneau de début** : Heure de début de la réservation (par exemple, "9:00").
- **Créneau de fin** : Heure de fin de la réservation (par exemple, "11:00").
- La journée est découpée en créneaux d'une heure (24 créneaux au total).
- Les réservations ne peuvent pas s'étendre sur plusieurs jours.

## Endpoints

### Lister des salles
