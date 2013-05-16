package com.greatguide.backend.core.IGGService;

/***
 * Author: Lennie De Villiers
 * Created: 27 Nov 2012
*/
interface IGGService {

		void destroy();
        void playAudio();
        boolean isPlaying();
        void pause();
        void resume();
        void stop();

        void increaseVolume();
        void decreaseVolume();

        void repeatForever();
        void stopRepeatForever();
        void sendLocation(in Location aLocation);
}