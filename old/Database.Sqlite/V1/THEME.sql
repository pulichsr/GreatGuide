

create table THEME
(
  ID integer primary key,
  NAME text,
  DESCRIPTION text
);




create view VW_THEME
as select
  ID,
  NAME,
  DESCRIPTION
from THEME;


