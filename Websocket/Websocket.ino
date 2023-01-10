// conect to server, send message and display response
#include <ESP8266WiFi.h>
#include <SPI.h>
#include <LinkedList.h>

#ifndef STASSID
#define STASSID "ThuisgroepOW"
#define STAPSK  "sukkel67919772!"
#endif


const char* ssid     = STASSID;
const char* password = STAPSK;
const char* id = "1";
byte server[] = { 192, 168, 2, 24 }; // ip van server (C#)
int serverPort=10000;
WiFiClient client;

void setup()
{
  WiFi.mode(WIFI_STA);
  WiFi.begin(ssid, password);
  Serial.begin(115200);
  delay(1000);
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
