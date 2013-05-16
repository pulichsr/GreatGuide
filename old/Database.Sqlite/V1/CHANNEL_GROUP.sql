

create table CHANNEL_GROUP
(
  ID integer primary key,
  NAME text
);


create view VW_CHANNEL_GROUP
as select
  ID,
  NAME
from CHANNEL_GROUP;




