LinkedList<int> pinTypes = LinkedList<int>();
LinkedList<String> TempStrings = LinkedList<String>();


String payload;
bool GotPins = false;
bool pinSt=false;

void SetupStateManager() {
  if (client.connect(server, serverPort)) {
    Serial.println("connected");
    // transmit data
    //signup arduino with own IP;
    client.println("SetID:" + (String)id + ";" + "SetIP:" + WiFi.localIP().toString() + ";" + "$");
    Serial.println("SetID:" + (String)id + ";" + "SetIP:" + WiFi.localIP().toString() + ";" + "$");
    delay(250);
    //GetPins();
  } else {
    Serial.println("connection failed");
  }
}


void GetPins() {
  Serial.println(" get pins" );                      //-------------------------------------
  client.println("GetPins:" + (String)id + ";" + "$");
  String message = "";
  while (!GotPins)
    if (client.available()) {
      char c = client.read();
      if (c != '$') {
        Serial.print("!=$");Serial.println(c);                       //-------------------------------------
        if (c != ';') {
          Serial.print("!=;");Serial.println(c);                     //-------------------------------------
          message += c;
          //Serial.print(c);
        } else {
          Serial.println(" else ;" );              //-------------------------------------
          Serial.print("add");Serial.println(message);
          TempStrings.add(message);
          message = "";
        }
      } else {
        Serial.println("else $");                 //------------------------------------- 
        
        for (int i = 0; i < TempStrings.size(); i++) {
          Serial.println(TempStrings.get(i));
          int from = TempStrings.get(i).indexOf(':');
          int to = TempStrings.get(i).length();
          String subString = TempStrings.get(i).substring(from + 1, to);
          if (subString[0] == 'A') {
            pinTypes.add(0);
          } else if (subString[0] == 'D') {
            pinTypes.add(1);
          } else if (subString[0] == 'V') {
            pinTypes.add(2);
          }
          subString.remove(0, 1);

          if (subString[subString.length() - 1] == 'I') {
            subString.remove(subString.length() - 1);
            pinMode(subString.toInt(), INPUT);
            Serial.print(" INPUT " );Serial.println(subString.toInt());
          } else if (subString[subString.length() - 1] == 'O') {
            subString.remove(subString.length() - 1);
            pinMode(subString.toInt(), OUTPUT);
            Serial.print(" OUTPUT " );Serial.println(subString.toInt());
          } else if (subString[subString.length() - 1] == 'N') {
          }
        } 
        GotPins = true;
        Serial.println("Pins Setup");                //-------------------------------------
      }
    }
  TempStrings.clear();
  pinTypes[0]=0; pinTypes[1]=1; pinTypes[2]=2;
  pinMode(4,OUTPUT); pinMode(2,OUTPUT); pinMode(35,INPUT);
  Serial.print("pintypes0 ");Serial.println(pinTypes[0]);
  Serial.print("pintypes1 ");Serial.println(pinTypes[1]);
  Serial.print("pintypes2 ");Serial.println(pinTypes[2]);
}

String message = "";

void(* resetFunc) (void) = 0; //declare reset function @ address 0
 
void CheckStates() {
  //Serial.println(" checkStates " );                    //-------------------------------------
  //Hier komt ie elke keer als bij ArduinoDotNet
  if (!client.connected()) {
    Serial.println();
    Serial.println("disconnecting.");
    client.stop();
    resetFunc();  //call reset
    delay(100000);
  }
  if (client.available()) {
    char c = client.read();
    Serial.print(" c   ");Serial.println(c);
    if (c != '$') {
      if (c=='0') {digitalWrite(2,LOW);} 
      if (c=='1') {digitalWrite(2,HIGH);}
      if (c != ';') {
       }
      }
        /*
        message += c;
      } else {
        TempStrings.add(message);
        Serial.print("tempStrings ");Serial.println(TempStrings[0]); //---------------------------
        message = "";
      }
    } else {
      if (TempStrings.size() > 0) {
        for (int i = 0; i < TempStrings.size(); i++) {
          if(TempStrings.get(i).indexOf('|')){
            int fromcmd = TempStrings.get(i).indexOf(':');
            int fromsep = TempStrings.get(i).indexOf('|');
            int to = TempStrings.get(i).length();
            String subStringValue = TempStrings.get(i).substring(fromcmd + 1, fromsep);
            String subStringValue2 = TempStrings.get(i).substring(fromsep + 1, to);
            String subString = TempStrings.get(i).substring(0, fromcmd);
            Serial.println(subString);
            Serial.println(subStringValue);
            Serial.println(subStringValue2);
            if(subString == "SendPin"){
              if(subStringValue2 == "1"){
                digitalWrite(subStringValue.toInt(), HIGH);
              }
              else if (subStringValue2 == "0")   {
                digitalWrite(subStringValue.toInt(), LOW);
              }           
            }
          }
          else{
            int from = TempStrings.get(i).indexOf(':');
            int to = TempStrings.get(i).length();
            String subStringValue = TempStrings.get(i).substring(from + 1, to);
            String subString = TempStrings.get(i).substring(0, from);
            Serial.println(subString);
            Serial.println(subStringValue);
          }
        }
      }
      TempStrings.clear();
    }*/
  }//einde clientavailable

}
