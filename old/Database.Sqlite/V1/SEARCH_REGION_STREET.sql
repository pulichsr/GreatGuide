CREATE TABLE SEARCH_REGION_STREET
(
  "REGION_ID" integer not null,
  "STREET_ID" integer not null
);

CREATE  INDEX "SRS_REGION" ON "SEARCH_REGION_STREET" ("REGION_ID" ASC);

