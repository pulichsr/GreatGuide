create table ITINERARY_DAY
(
	ID integer not null primary key,
	ITINERARY_DAT date,
	COMMENT text
);

create view VW_ITINERARY_DAY
as select
	ID,
	ITINERARY_DAT as ITINERARYDAT,
	COMMENT
from ITINERARY_DAY;