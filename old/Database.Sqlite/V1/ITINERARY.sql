create table ITINERARY
(
	ID integer not null primary key,
	FIRST_NAME text,
	LAST_NAME text,
	TITLE text,
	GRACE_PERIOD smallint,
	BOOKING_REFERENCE text,
	GEO_FENCE_DATA text,
	ARRIVAL_DAT datetime,
	DEPARTURE_DAT datetime,
	BRANDING1 text,
	BRANDING2 text,
	BRANDING3 text
);

create view VW_ITINERARY
as select
	ID,
	FIRST_NAME as FIRSTNAME,
	LAST_NAME as LASTNAME,
	TITLE,
	GRACE_PERIOD as GRACEPERIOD,
	BOOKING_REFERENCE as BOOKINGREFERENCE,
	GEO_FENCE_DATA as GEOFENCEDATA,
	ARRIVAL_DAT as ARRIVALDAT,
	DEPARTURE_DAT as DEPARTUREDAT,
	BRANDING1,
	BRANDING2,
	BRANDING3
from ITINERARY;