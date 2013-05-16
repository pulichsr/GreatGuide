

create table CHANNEL
(
  ID integer primary key,
  CHANNEL_GROUP_ID integer,
  CONTENT_PATH text,
  LANGUAGE text
);


create index CHNL_GROUP on CHANNEL(CHANNEL_GROUP_ID);


create view VW_CHANNEL
as select
  ID,
  CHANNEL_GROUP_ID as CHANNELGROUPID,
  CONTENT_PATH as CONTENTPATH,
  LANGUAGE
from CHANNEL;



