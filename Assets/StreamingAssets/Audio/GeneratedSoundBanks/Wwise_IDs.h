/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PLAY_AMB = 2959533290U;
        static const AkUniqueID PLAY_BOUNCE = 3465618802U;
        static const AkUniqueID PLAY_FTPS = 3079036743U;
        static const AkUniqueID PLAY_GAME = 216639764U;
        static const AkUniqueID PLAY_JAWCLOSE = 3061594324U;
        static const AkUniqueID PLAY_JUMP = 3689126666U;
        static const AkUniqueID PLAY_MISS = 1631400290U;
        static const AkUniqueID PLAY_MOUTH_TRANSITION = 4273929195U;
        static const AkUniqueID PLAY_MUSIC = 2932040671U;
        static const AkUniqueID PLAY_PLAYER_KILLED = 1115105435U;
        static const AkUniqueID PLAY_UI = 2044747472U;
        static const AkUniqueID PLAY_VICTORY = 2453267296U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace MUSIC_STATE
        {
            static const AkUniqueID GROUP = 3826569560U;

            namespace STATE
            {
                static const AkUniqueID INGAME = 984691642U;
                static const AkUniqueID MENU = 2607556080U;
                static const AkUniqueID VICTORY = 2716678721U;
            } // namespace STATE
        } // namespace MUSIC_STATE

        namespace PLAYER_MUSIC
        {
            static const AkUniqueID GROUP = 889723386U;

            namespace STATE
            {
                static const AkUniqueID PLAYER1 = 2188949039U;
                static const AkUniqueID PLAYER2 = 2188949036U;
            } // namespace STATE
        } // namespace PLAYER_MUSIC

    } // namespace STATES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID MUSIC_SPEED = 2537816454U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID SOUNDBANK = 1661994096U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID PLAYERS_MUSIC = 187937321U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
