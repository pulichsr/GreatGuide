create table DESTINATION_COLLECTION
(
	ID integer not null primary key,
	NAME text,
	CODE text
);

create view VW_DESTINATION_COLLECTION
as select
	ID,
	NAME,
	CODE
from DESTINATION_COLLECTION;
