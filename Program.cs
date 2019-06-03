using System;
using System.Threading;
using System.Net.Http;
using System.Collections.Generic;

namespace sensorUnitSim
{
    // class definitions
    class TemperatureSensor {
        public string temperatureReading {get; set; }
        public string readingTime {get; set; } // should be epoch time in ms
    }

    class HumiditySensor {
        public string humidityReading {get; set; }
        public string readingTime {get; set; }
    }

    class AccelerometerSensor {
        public string xReading {get; set; }
        public string yReading {get; set; }
        public string zReading {get; set; }
        public string readingTime {get; set; }
    }

    class GPSSensor {
        public string latitudeReading {get; set; }
        public string longitudeReading {get; set; }
        public string readingTime {get; set; }
    }

    class HeartRateSensor {
        public string bpmReading {get; set; }
        public string readingTime {get; set; }
    }

    class Program {
        static public float randomNumber(float min, float max) {  
            Random random = new Random();
            if(min >= max) {
                max = min;
                min = max * -1;
            } 
            Console.WriteLine(random.Next((int)min, (int)max));  
            return random.Next((int)min, (int)max);  
        } 

        static string randomizeValue(string currentReading) {
            float conversion = float.Parse(currentReading);
            conversion += (randomNumber(-1 * (int)conversion, (int)conversion)/100);           
            return conversion.ToString();
        }

        private static readonly HttpClient client = new HttpClient();

        static async System.Threading.Tasks.Task Main(string[] args) {
            
            // instantiate system objects
            DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
            HttpClient client = new HttpClient();
            
            // produce objects
            TemperatureSensor produceTemperature = new TemperatureSensor();
            HumiditySensor produceHumidity = new HumiditySensor();
            AccelerometerSensor produceAccelerometer = new AccelerometerSensor();
            GPSSensor produceGPS = new GPSSensor();

            // livestock objects
            TemperatureSensor livestockTemperature = new TemperatureSensor();
            HeartRateSensor livestockHeartRate = new HeartRateSensor();

            // initialize values
            string URL = "http://210.10.227.40:3000/api/";

            produceTemperature.temperatureReading = "4";
            produceHumidity.humidityReading = "20";
            produceAccelerometer.xReading = "0.51";
            produceAccelerometer.yReading = "-0.59";
            produceAccelerometer.zReading = "4.31";
            produceGPS.latitudeReading = "-37.804663448";
            produceGPS.longitudeReading = "144.957996168";

            livestockTemperature.temperatureReading = "23";
            livestockHeartRate.bpmReading = "60";


            // initialize schema data for produce
            var produceTemperatureReadings = new Dictionary<string, string> {
                {"$class", "org.capstone.TemperatureReading"},
                {"readingTime", ""},
                {"centigrade", "24.9"},
                {"shipment", "resource:org.capstone.Shipment#IBBY"},
                {"transactionId", ""},
                {"timestamp", "0000-01-01T00:00:00.000Z"}
            };

            var produceHumidityReadings = new Dictionary<string, string> {
                {"$class", "org.capstone.HumidityReading"},
                {"readingTime", ""},
                {"humidity", ""},
                {"shipment", "resource:org.capstone.Shipment#IBBY"},
                {"transactionId", ""},
                {"timestamp", "0000-01-01T00:00:00.000Z"}
            };

            var produceAccelerometerReadings = new Dictionary<string, string> {
                {"$class", "org.capstone.AccelerometerReading"},
                {"readingTime", ""},
                {"x", ""},
                {"y", ""},
                {"z", ""},
                {"shipment", "resource:org.capstone.Shipment#IBBY"},
                {"transactionId", ""},
                {"timestamp", "0000-01-01T00:00:00.000Z"}
            };

            var produceGPSReadings = new Dictionary<string, string> {
                {"$class", "org.capstone.GpsReading"},
                {"readingTime", ""},
                {"latitude", ""},
                {"longitude", ""},
                {"shipment", "resource:org.capstone.Shipment#IBBY"},
                {"transactionId", ""},
                {"timestamp", "0000-01-01T00:00:00.000Z"}
            };


            // initialize schema data for livestock
            var livestockTemperatureReadings = new Dictionary<string, string> {
                {"$class", "org.capstone.TemperatureReading"},
                {"readingTime", ""},
                {"centigrade", ""},
                {"shipment", "resource:org.capstone.Shipment#ALEXEI"},
                {"transactionId", ""},
                {"timestamp", "0000-01-01T00:00:00.000Z"}
            };

            var livestockHeartRateReadings = new Dictionary<string, string> {
                {"$class", "org.capstone.HeartRateReading"},
                {"readingTime", ""},
                {"bpm", ""},
                {"shipment", "resource:org.capstone.Shipment#ALEXEI"},
                {"transactionId", ""},
                {"timestamp", "0000-01-01T00:00:00.000Z"}
            };
            




            // lets go!
            Console.WriteLine("start");

            while(true) {
                // update readings
                dto = new DateTimeOffset(DateTime.Now);

                livestockTemperature.temperatureReading = randomizeValue(livestockTemperature.temperatureReading);
                livestockTemperature.readingTime = dto.ToUnixTimeMilliseconds().ToString();
                livestockHeartRate.bpmReading = randomizeValue(livestockHeartRate.bpmReading);
                livestockHeartRate.readingTime = dto.ToUnixTimeMilliseconds().ToString();


                produceTemperature.temperatureReading = randomizeValue(produceTemperature.temperatureReading);
                produceTemperature.readingTime = dto.ToUnixTimeMilliseconds().ToString();
                produceHumidity.humidityReading = randomizeValue(produceHumidity.humidityReading);
                produceHumidity.readingTime = dto.ToUnixTimeMilliseconds().ToString();
                produceAccelerometer.xReading = randomizeValue(produceAccelerometer.xReading);
                produceAccelerometer.readingTime = dto.ToUnixTimeMilliseconds().ToString();
                produceAccelerometer.yReading = randomizeValue(produceAccelerometer.yReading);
                produceAccelerometer.readingTime = dto.ToUnixTimeMilliseconds().ToString();
                produceAccelerometer.zReading = randomizeValue(produceAccelerometer.zReading);
                produceAccelerometer.readingTime = dto.ToUnixTimeMilliseconds().ToString();
                produceGPS.latitudeReading = randomizeValue(produceGPS.latitudeReading);
                produceGPS.readingTime = dto.ToUnixTimeMilliseconds().ToString();
                produceGPS.longitudeReading = randomizeValue(produceGPS.longitudeReading);
                produceGPS.readingTime = dto.ToUnixTimeMilliseconds().ToString();


                // enter data into http request schema dictionary
                livestockTemperatureReadings["centigrade"] = livestockTemperature.temperatureReading;
                livestockTemperatureReadings["readingTime"] = livestockTemperature.readingTime;
                livestockHeartRateReadings["bpm"] = livestockHeartRate.bpmReading;
                livestockHeartRateReadings["readingTime"] = livestockHeartRate.readingTime;

                produceTemperatureReadings["centigrade"] = produceTemperature.temperatureReading;
                produceTemperatureReadings["readingTime"] = produceTemperature.readingTime;
                produceHumidityReadings["humidity"] = produceHumidity.humidityReading;
                produceHumidityReadings["readingTime"] = produceHumidity.readingTime;
                produceAccelerometerReadings["x"] = produceAccelerometer.xReading;
                produceAccelerometerReadings["y"] = produceAccelerometer.yReading;
                produceAccelerometerReadings["z"] = produceAccelerometer.zReading;
                produceAccelerometerReadings["readingTime"] = produceAccelerometer.readingTime;
                produceGPSReadings["latitude"] = produceGPS.latitudeReading;
                produceGPSReadings["longitude"] = produceGPS.longitudeReading;
                produceGPSReadings["readingTime"] = produceGPS.readingTime;


                // http request stuff...

                var content = new FormUrlEncodedContent(livestockTemperatureReadings);
                var response = await client.PostAsync(URL + "TemperatureReading", content);
                var responseString = await response.Content.ReadAsStringAsync();

                var content1 = new FormUrlEncodedContent(livestockHeartRateReadings);
                var response1 = await client.PostAsync(URL + "HeartRateReading", content1);
                var responseString1 = await response1.Content.ReadAsStringAsync();



                var content2 = new FormUrlEncodedContent(produceTemperatureReadings);
                var response2 = await client.PostAsync(URL + "TemperatureReading", content2);
                var responseString2 = await response2.Content.ReadAsStringAsync();

                var content3 = new FormUrlEncodedContent(produceHumidityReadings);
                var response3 = await client.PostAsync(URL + "HumidityReading", content3);
                var responseString3 = await response3.Content.ReadAsStringAsync();

                var content4 = new FormUrlEncodedContent(produceAccelerometerReadings);
                var response4 = await client.PostAsync(URL + "AccelerometerReading", content4);
                var responseString4 = await response4.Content.ReadAsStringAsync();

                var content5 = new FormUrlEncodedContent(produceGPSReadings);
                var response5 = await client.PostAsync(URL + "GpsReading", content5);
                var responseString5 = await response5.Content.ReadAsStringAsync();


                Console.WriteLine("done at " + livestockTemperature.readingTime);


                // pause for a 20 seconds
                Thread.Sleep(20000);                
            }            
        }
    }
}
