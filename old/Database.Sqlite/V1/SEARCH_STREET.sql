CREATE TABLE SEARCH_STREET
(
  "ID" integer not null primary key,
  "REGION_ID" integer,
  "SEARCH_KEY" VARCHAR NOT NULL ,
  "NAME" VARCHAR NOT NULL , 
  "COLLATED_NAME" VARCHAR NOT NULL,
  "LATITUDE" double,
  "LONGITUDE" double,
  "STREET_NUMBERS" varchar
);

CREATE  INDEX "SS_SEARCH_KEY" ON "SEARCH_STREET" ("SEARCH_KEY" ASC);
CREATE  INDEX "SS_REGION" ON "SEARCH_STREET" ("REGION_ID" ASC);

