package com.greatguide.test;

import android.test.ActivityInstrumentationTestCase2;
import com.greatguide.ui.UnitTestActivity;
import com.greatguide.backend.core.LocationUtils;

/**
 *
 * Unit test from LocationUtils
 *
 * Author: Lennie De Villiers
 * Created: 21 Nov 2012
 */
public class LocationUtilsTest extends ActivityInstrumentationTestCase2<UnitTestActivity> {

    // Location: Lennie house - 21 Pinewood Avenue, Goodwood
    private final double START_LAT = -33.89698D;
    private final double START_LONG = 18.53567D;

    // Location: MWEB Cape Town Office
    private final double DEST_LAT = -33.89110D;
    private final double DEST_LONG = 18.56633D;

    // Answer to the questions for assertion
    private final float DISTANCE_ASSERT = 2910.0593f;
    private final float AVERAGE_WALK_SEC_ASSERT = 2078.61372f;
    private final float AVERAGE_WALK_MIN_ASSERT = 34.643562f;
    private final float METERS_TO_KILOMETERS_ASSERT = 2.9100593f;

    public LocationUtilsTest() {
        super("com.greatguide", UnitTestActivity.class);
    }

    public LocationUtilsTest(Class<UnitTestActivity> activityClass) {
        super(activityClass);
    }

    protected void setUp() throws Exception {
        super.setUp();
    }

    protected void tearDown() throws Exception {
        super.tearDown();
    }

    public void testCalculateDistance() {
        float distance = LocationUtils.calculateDistance(START_LAT, START_LONG, DEST_LAT, DEST_LONG);
        assertEquals(DISTANCE_ASSERT, distance);
    }

    public void testCalculateAverageWalkingSpeedInSeconds()
    {
        float timeInSeconds = LocationUtils.calculateAverageWalkingSpeedInSeconds(START_LAT, START_LONG, DEST_LAT, DEST_LONG);
        assertEquals(AVERAGE_WALK_SEC_ASSERT, timeInSeconds);
    }

    public void testCalculateAverageWalkingSpeedInMinutes()
    {
        float timeInMinutes = LocationUtils.calculateAverageWalkingSpeedInMinutes(START_LAT, START_LONG, DEST_LAT, DEST_LONG);
        assertEquals(AVERAGE_WALK_MIN_ASSERT, timeInMinutes);
    }

    public void testMetersToKilometers()
    {
        float distance = LocationUtils.calculateDistance(START_LAT, START_LONG, DEST_LAT, DEST_LONG);
        float km = LocationUtils.metersToKilometers(distance);
        assertEquals(METERS_TO_KILOMETERS_ASSERT, km);
    }
}
