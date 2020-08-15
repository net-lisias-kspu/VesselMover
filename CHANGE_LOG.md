# Vessel Mover :: Change Log

* 2020-0217: 1.9.0 (jrodriguez) for KSP 1.8.0
	+ New:
		- Recompiled for KSP 1.9.0
* 2019-1022: 1.8.0 (jrodriguez) for KSP 1.7.1
	+ Recompiled for KSP 1.8.0
	+ Fixes issue regarding vessel with robotic arms causing infinite flying off
* 2019-0609: 1.7.5 (jrodriguez) for KSP 1.7
	+ New:
		- Recompiled for KSP 1.7.1
* 2019-0422: 1.7.4 (jrodriguez) for KSP 1.7
	+ New:
		- Recompiled for KSP 1.7
		- (Thanks to DoctorDavinci) Added new Feature:  Move Launch  Now you can Move to a specific location prior to actually spawning a vessel.
		- (Thanks to DoctorDavinci) Added GPS location Spawning.  With Move Launch you can spawn a vessel to a specific GPS location.
* 2018-0514: 1.7.3 (Papa_Joe) for KSP 1.4.5
	+ Add Crew selection screen to spawn feature.  You can now (optionally) select one crewmember from a list of available crew. Git Issue #17
	+ Fixed Tab key movement mode changes to prevent positioning craft underground. Git Issue #29
	+ Clean up tool tips to display above window and not beyond edge of screen.
	+ Improved control of altitude during movement mode changes. Now when changing altitudes, changes to movement mode will not alter current altitude.
	+ Added new Movement Mode altitude reset key Throttle Cuttoff (Default is X) to allow for flexible altitude positioning.
	+ Revised help and readme to reflect changes to hot keys.
* 2018-0427: 1.7.2.2 (Papa_Joe) for KSP 1.4.2
	+ Recompiled for KSP 1.4.3  Works in 1.4.x
	+ Fined tuned altitude adjustment for better behavior at low speed modes.
* 2018-0426: 1.7.2.1 (Papa_Joe) for KSP 1.4.2
	+ Altered altitude adjustment to account for speed modes.  Height changes now vary with the mode.
	+ Fixed:  Choose Crew toggle not implemented yet.  Changed to hidden for now.
* 2018-0426: 1.7.2 (Papa_Joe) for KSP 1.4.2
	+ 1.7.2
		- Add altitude adjust keys to help window.
		- Add drop feature to Vessel Move.  Git Issue #3  Click Drop to allow vessel to free fall.
* 2018-0326: 1.7.1 (Papa_Joe) for KSP 1.4.1
	+ Add ability to move Vessel Mover main window. Git Issues #13 and #42
	+ Correct issue with Vessel Selection where the wrong game save folder is used.  Git Issue #31
	+ Make Window display honor show/hide UI display in flight.  Git issue #44
* 2018-0326: 1.7 (Papa_Joe) for KSP 1.4.1
	+ 1.4 recompile and upgraded to latest Unity
	+ Updated AppLauncher toolbar button to support new Unity Compression.  Increased icon size to 128 x 128
* 2017-1023: 1.6.2 (Papa_Joe) for KSP 1.2
	+ No changelog provided
* 2017-0706: 1.6.1 (Papa_Joe) for KSP 1.2
	+ Adding option to not spawn crew.
* 2017-0602: 1.6.0 (Papa_Joe) for KSP 1.2
	+ No changelog provided
* 2016-1110: 1.5.1.3 (Papa_Joe) for KSP 1.2
	+ 1.2.1 recompile
	+ adding ignore gforces call on move to stabalize vessels while moving
	+ Throttle up/down will now add/decrease altitude on move
	+ Fixed: issue of vessel spawn in 1.2 would ignore water detection and spawn vessel on ocean floor
* 2016-0916: 1.5.1.2 (Papa_Joe) for KSP 1.1.3 PRE-RELEASE
	+ Updated to be compatible with KSP 1.2pr.  This is prerelease software.  not for official distribution.
* 2016-0808: 1.5.1.1 (Papa_Joe) for KSP 1.1.3
	+ Fixed Git Issue #1 Vessels taking fall damage when placed.  Added new vessel movement mode that allows Fine control and low altitude (5 meters) placement.  Should eliminate fall damage.
* 2016-0724: 1.5.1 (Papa_Joe) for KSP 1.1.3
	+ KSP 1.1.3 compatibility
	+ Fixed: Bug in Spawn Vessel where kerbal created was type Unowned.  would get lost and would disappear.  Changed to Type Crew.
	+ Continuation of BahamutoD's work.
* 2016-0724: 1.5.0 (Papa_Joe) for KSP 0.7.3
	+ No changelog provided
* 2016-0425: 1.5 (BahamutoD) for KSP 0.7.3
	+ KSP 1.1 compatibility
* 2016-0304: 1.4 (BahamutoD) for KSP 0.90
	+ Toolbar menu
	+ Disabled alt-p hotkey (please use toolbar UI)
	+ Vessel spawner (experimental)
* 2016-0218: 1.3 (BahamutoD) for KSP 0.90
	+ Prevent pickup when vessel is packed (causes vessel to drop when released)
