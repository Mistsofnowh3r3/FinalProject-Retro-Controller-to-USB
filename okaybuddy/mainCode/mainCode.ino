#include <N64Pad.h> // https://github.com/SukkoPera/N64PadForArduino
#include <Joystick.h>
#include <string.h>
#include <Keyboard.h> 



// Globals //

String serialNow = "";
int button_values[] = {120, 121, 32, 99, KEY_UP_ARROW, KEY_DOWN_ARROW, KEY_LEFT_ARROW, KEY_RIGHT_ARROW, 122, KEY_LEFT_CTRL, 154, 162};

//various mode related things
bool nesMode = false; 
bool snesMode = false; 
bool n64Mode = false; 
bool serialMode = true;

// modes for what the poutput will be 
bool keyboardMode = true; //output as keyboard
bool controllerMode = false; //output as controller

// Constants //

//n64stuff
const byte ANALOG_DEAD_ZONE = 20U;
const int8_t ANALOG_MIN_VALUE = -80;
const int8_t ANALOG_MAX_VALUE = 80;
const int8_t ANALOG_IDLE_VALUE = 0;

N64Pad pad;

Joystick_ usbStick (
	JOYSTICK_DEFAULT_REPORT_ID,
	JOYSTICK_TYPE_JOYSTICK,
	10,			// buttonCount
	0,			// hatSwitchCount (0-2)
	true,		// includeXAxis
	true,		// includeYAxis
	false,		// includeZAxis
	true,		// includeRxAxis
	true,		// includeRyAxis
	false,		// includeRzAxis
	false,		// includeRudder
	false,		// includeThrottle
	false,		// includeAccelerator
	false,		// includeBrake
	false		// includeSteering
);

bool mapAnalogToDPad = false;

#define deadify(var, thres) (abs (var) > thres ? (var) : 0)



// Controller I/O Pins
const int pulsePinNes = 0; 
const int latchPinNes = 1; 
const int dataPinNes = 2; 
//const int dataPinN64 = 3; //the n64 pad thing use pin 3 , this assingment is symbolic
const int pulsePinSnes = 4; 
const int latchPinSnes = 5; 
const int dataPinSnes = 6; 

// LED Indicator Pins
const int nesIndicateLed = 7;
const int snesIndicateLed = 8;
const int n64IndicateLed = 9;

//Switch ins
// SERIAL | NES | SNES | N64

const int serialSwitch = 10; 
const int nesSwitch = 11; 
const int snesSwitch = 12; 
const int n64Switch = 13; 








void flashLed (byte n) {
	for (byte i = 0; i < n; ++i) {
		digitalWrite (LED_BUILTIN, LOW);
		delay (40);
		digitalWrite (LED_BUILTIN, HIGH);
		delay (80);
	}
}

void checkSerialForEnd() {
    serialNow = Serial.readStringUntil('!');
    if (serialNow != "STOP") { //check for a serial request
        return; // else just continue
    }
    Serial.write("9999999999999999999999999999999999999999999999999999999999999999");
    Serial.write("9999999999999999999999999999999999999999999999999999999999999999");
    Serial.write("9999999999999999999999999999999999999999999999999999999999999999");
    Serial.write("9999999999999999999999999999999999999999999999999999999999999999");
    delay(120);
    Serial.flush();
    Serial.write("All done, ready to move on to the next.");
    loop(); //if there is a stop message, exit to the main loop
    
}

void checkButton(int button, bool mode) {

    // if mode = 0 check for press if 1 check for release
    //1 = A    
    //2 = B
    //3 = SELECT
    //4 = START
    //5 = UP
    //6 = DOWN
    //7 = LEFT
    //8 = RIGHT
    //snes
    //9 = A
    //10 = X
    //11 = L
    //12 = R
    
    int button_index = button - 1;
      
    if (button_index > 12) { // if we have passed all the buttons just return and do nothing
        return;
    }

    if(nesMode) {
        if ((mode == false) && (digitalRead(dataPinNes) == false)) {
            Keyboard.press(button_values[button_index]);
            Serial.print(button_values[button_index]);
            return;
        }
        if ((mode == true) && (digitalRead(dataPinNes) == true)) {
            Keyboard.release(button_values[button_index]);
            return;
        }
    }
    if (snesMode) {
        if ((mode == false) && (digitalRead(dataPinSnes) == false)) {
            Keyboard.press(button_values[button_index]);
            Serial.print(button_values[button_index]);
            return;
        }
        if ((mode == true) && (digitalRead(dataPinSnes) == true)) {
            Keyboard.release(button_values[button_index]);
            return;
        }
    }
}

void serialActions() {

    serialNow = Serial.readStringUntil('!');
    if (serialNow != "SREQ") { //check for a serial request
        return; // there was no request, return
    }
    Serial.write("SACK"); //send acknowledge with the delimiter
    while (true){ //wait for a command
        serialNow = Serial.readStringUntil('!');

        if (serialNow == "REMAP") { // the command is remap

            Serial.write("REMAPACK"); // ackowledge the command

            while (true) {  // wait for a system identifier 

                serialNow = Serial.readStringUntil('!');

                if (serialNow == "NES") { 
                    Serial.write("NESACK");

                    while(true){
                        serialNow = Serial.readStringUntil('!');
                        Serial.write("Got: ");
                        Serial.write(serialNow.c_str());
                        checkSerialForEnd();
                    }
                }
                if (serialNow == "SNES") {
                    Serial.write("SNESACK");

                    while(true){
                        serialNow = Serial.readStringUntil('!');
                        Serial.write("Got: ");
                        Serial.write(serialNow.c_str());
                        if (serialNow == "DONE"){
                            Serial.write("Ready for something else.");
                            return;
                        }
                        checkSerialForEnd();
                    }
                }
                if (serialNow == "N64") {
                    Serial.write("N64ACK");

                    while(true){
                        serialNow = Serial.readStringUntil('!');
                        Serial.write("Got: ");
                        Serial.write(serialNow.c_str());
                        if (serialNow == "DONE"){
                            Serial.write("Ready for something else.");
                            return;
                        }
                        checkSerialForEnd();
                    }
                }
                checkSerialForEnd();
            }   
        }
        checkSerialForEnd();
    }
}

void nes() {
    digitalWrite(latchPinNes, HIGH);
    checkButton(1, 0); // check for A here
    checkButton(1, 1);

    delayMicroseconds(12);

    digitalWrite(latchPinNes, LOW);
    delayMicroseconds(6);
    

    for (int j = 2; j < 10; j++)
    {
        // pulse the pulse pin

        //6 high
        digitalWrite(pulsePinNes, HIGH);
        // check for the rest of the buttons
        checkButton(j, 0); 
        checkButton(j, 1);
        delayMicroseconds(6);

        //6 low
        digitalWrite(pulsePinNes, LOW);
        delayMicroseconds(6);
    }

}   

void snes() {
    digitalWrite(latchPinSnes, HIGH);
    checkButton(1, false); 
    checkButton(1, true);

    delayMicroseconds(12);

    digitalWrite(latchPinSnes, LOW);
    delayMicroseconds(6);
    

    for (int j = 2; j < 18; j++)
    {
        // pulse the pulse pin

        //6 high
        digitalWrite(pulsePinSnes, HIGH);
        // check for the rest of the buttons
        checkButton(j, false); 
        checkButton(j, true);
        delayMicroseconds(6);

        //6 low
        digitalWrite(pulsePinSnes, LOW);
        delayMicroseconds(6);
    }
}  

void n64() {
   static boolean haveController = false;

	if (!haveController) {
		if (pad.begin()) {
		    // Controller detected!
			digitalWrite(n64IndicateLed, HIGH);
            Serial.write("N64 Controller Connected.");
			haveController = true;
		} 
        else {
            digitalWrite(n64IndicateLed, LOW);
			delay(333);
		}

	} else {
		if (!pad.read()) {
			// Controller lost :(
			//digitalWrite (LED_BUILTIN, LOW);
			haveController = false;
		} 
        else {
			// Controller was read fine
        
			// Map buttons!
            //if ((pad.buttons & N64Pad::BTN_B) != 0) {
            //   usbStick.setButton (0, 1);
            //}
            //if ((pad.buttons & N64Pad::BTN_B) == 0) {
            //    usbStick.setButton (0, 0);
            //}
			usbStick.setButton(0, (pad.buttons & N64Pad::BTN_B) != 0);
			usbStick.setButton(1, (pad.buttons & N64Pad::BTN_A) != 0);
			usbStick.setButton(2, (pad.buttons & N64Pad::BTN_C_LEFT) != 0);
			usbStick.setButton(3, (pad.buttons & N64Pad::BTN_C_DOWN) != 0);
			usbStick.setButton(4, (pad.buttons & N64Pad::BTN_C_UP) != 0);
			usbStick.setButton(5, (pad.buttons & N64Pad::BTN_C_RIGHT) != 0);
			usbStick.setButton(6, (pad.buttons & N64Pad::BTN_L) != 0);
			usbStick.setButton(7, (pad.buttons & N64Pad::BTN_R) != 0);
			usbStick.setButton(8, (pad.buttons & N64Pad::BTN_Z) != 0);
			usbStick.setButton(9, (pad.buttons & N64Pad::BTN_START) != 0);
			// D-Pad makes up the X/Y axes
			if ((pad.buttons & N64Pad::BTN_UP) != 0) {
				usbStick.setYAxis(ANALOG_MIN_VALUE);
			} 
            else if ((pad.buttons & N64Pad::BTN_DOWN) != 0) {
				usbStick.setYAxis(ANALOG_MAX_VALUE);
			} 
            else {
				usbStick.setYAxis(ANALOG_IDLE_VALUE);
			}
			if ((pad.buttons & N64Pad::BTN_LEFT) != 0) {
				usbStick.setXAxis(ANALOG_MIN_VALUE);
			} 
            else if ((pad.buttons & N64Pad::BTN_RIGHT) != 0) {
				usbStick.setXAxis(ANALOG_MAX_VALUE);
			} 
            else {
				usbStick.setXAxis(ANALOG_IDLE_VALUE);
			}
			// The analog stick gets mapped to the X/Y rotation axes
			usbStick.setRxAxis (pad.x);
			usbStick.setRyAxis (pad.y);
			
			// All done, send data for real!
			usbStick.sendState ();
			
		}
    } 
}

void checkSwitches() {
    if ( (!digitalRead(nesSwitch) + !digitalRead(snesSwitch) + !digitalRead(n64Switch)) > 1  ) { // if more then one is high somehow go to serial.
    Serial.write("Error: Too many selected.");
        serialMode = false;  //
        nesMode = false;
        snesMode = false;
        n64Mode = false;
    }
    else if (!digitalRead(nesSwitch)) {
        serialMode = false;
        nesMode = true;     //
        snesMode = false;
        n64Mode = false;
    }
    else if (!digitalRead(snesSwitch)) {
        serialMode = false;
        nesMode = false;
        snesMode = true;    //
        n64Mode = false;
    }
    else if (!digitalRead(n64Switch)) {
        serialMode = false;
        nesMode = false;
        snesMode = false;
        n64Mode = true;     //
    }
    else if (!digitalRead(serialSwitch)) {
        serialMode = true; // 
        nesMode = false;
        snesMode = false;
        n64Mode = false;
    }
    else {//fallback to serial
        serialMode = true; 
        nesMode = false;
        snesMode = false;
        n64Mode = false;
    }
}

void setup() {
    Serial.begin(9600);
    // set the digital pin STATES
    pinMode(pulsePinNes, OUTPUT);
    pinMode(latchPinNes, OUTPUT);
    pinMode(dataPinNes, INPUT);

    pinMode(pulsePinSnes, OUTPUT);
    pinMode(latchPinSnes, OUTPUT);
    pinMode(dataPinSnes, INPUT);

    pinMode(serialSwitch, INPUT_PULLUP);
    pinMode(nesSwitch, INPUT_PULLUP); 
    pinMode(snesSwitch, INPUT_PULLUP);
    pinMode(n64Switch, INPUT_PULLUP);

    pinMode(nesIndicateLed, OUTPUT);
    pinMode(snesIndicateLed, OUTPUT);
    pinMode(n64IndicateLed, OUTPUT);

    usbStick.begin (false);		// We'll call sendState() manually to minimize lag
	usbStick.setXAxisRange (ANALOG_MIN_VALUE, ANALOG_MAX_VALUE);
	usbStick.setYAxisRange (ANALOG_MIN_VALUE, ANALOG_MAX_VALUE);
	usbStick.setRxAxisRange (ANALOG_MIN_VALUE, ANALOG_MAX_VALUE);
	usbStick.setRyAxisRange (ANALOG_MAX_VALUE, ANALOG_MIN_VALUE);
}

void loop() {  
    checkSwitches();
    if (serialMode == true) serialActions();
    
    if (nesMode == true) nes();
    
    if (snesMode == true) snes();
    
    if (n64Mode == true) n64();
    
}