 // conect to server, send message and display response
#include <ESP8266WiFi.h>
#include <SPI.h>
#include <LinkedList.h>

#ifndef STASSID
//#define STASSID "Duckie_ZTE"
//#define STASSID "Duckie"
//#define STAPSK  "roErmrj1"
#define STASSID "Guests"
#define STAPSK  "grade!eight"
#endif


const char* ssid     = STASSID;
const char* password = STAPSK;
const char* id = "1";
//byte server[] = { 192, 168, 2, 16 }; // ip van server (C#)
byte server[] = { 172, 16, 222, 132 }; // ip van server (C#)
int serverPort=10000;
//int serverPort=8000;
WiFiClient client;

void setup()
{
  WiFi.mode(WIFI_STA);
  WiFi.begin(ssid, password);
  Serial.begin(115200);
  delay(1000);
  pinMode(2,OUTPUT);
  Serial.println("connecting...");
  // Ethernet ready, connect to server and transmit data
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  SetupStateManager();
}

// display response from server
void loop()
{
  CheckStates();
}
