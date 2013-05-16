CREATE VIEW VW_DESTINATION
as select
	ID,
	CODE,
	SHORT_DESCRIPTION as SHORTDESCRIPTION,
	LONG_DESCRIPTION as LONGDESCRIPTION,
	DESTINATION_TYPE_ID as DESTINATIONTYPEID,
	CLASSIFICATION_ID as CLASSIFICATIONID,
	ARRIVAL_ZONE_DATA as ARRIVALZONEDATA,
	ADDRESS,
	TELEPHONE_NO as TELEPHONENO,
	MASTER_AREA_ID as MASTERAREAID,
	THEME_ID as THEMEID,
	VERSION_NO as VERSIONNO,
	RECOMMENDATION,
	LOCATION,
	CONDITION,
	LATITUDE,
	LONGITUDE,
	CELL_NO as CELLNO,
	IMAGE1_FILENAME as IMAGE1FILENAME,
	IMAGE2_FILENAME as IMAGE2FILENAME,
	COMMENT1,
	COMMENT2,
	COMMENT3,
	COMMENT4,
	BOOKING,
	'LANGUAGE',
	COMMENT,
	COLLECTION_ID as COLLECTIONID,
	ARRIVAL_ZONE_TYPE as ARRIVALZONETYPE,
	ARRIVAL_ZONE_MIN_LATITUDE as ARRIVALZONEMINLATITUDE,
	ARRIVAL_ZONE_MAX_LATITUDE as ARRIVALZONEMAXLATITUDE,
	ARRIVAL_ZONE_MIN_LONGITUDE as ARRIVALZONEMINLONGITUDE,
	ARRIVAL_ZONE_MAX_LONGITUDE as ARRIVALZONEMAXLONGITUDE,
	GRID_REFERENCE_X as GRIDREFERENCEX,
	GRID_REFERENCE_Y as GRIDREFERENCEY,
	INCLUDE_IN_NEAREST_SEARCH as INCLUDEINNEARESTSEARCH,
        MORE_AD_IMAGE as MOREADIMAGE,
        MORE_AD_TEXT as MOREADTEXT

from DESTINATION