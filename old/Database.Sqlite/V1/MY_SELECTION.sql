create table MY_SELECTION
(
	DESTINATION_ID integer not null primary key
);

create view VW_MY_SELECTION
as select
	DESTINATION_ID as DESTINATIONID
from MY_SELECTION;