import os
import glob
import time
import sqlite3
import datetime
import RPi.GPIO as GPIO
import time
import sys

GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)
led_list = [26,21,20,16,12,25,24,23,18]
GPIO.setup(led_list, GPIO.OUT)

os.system('modprobe w1-gpio')
os.system('modprobe w1-therm')

lower_attic_sensor = '28-031636b23aff'
sensor_list = {1:'28-031635fb46ff',2:'28-0416361444ff',3:'28-031635fff1ff',4:'28-03163622ebff', 5:'28-031636b23aff', 6:'28-0416374e05ff'}

base_dir = '/sys/bus/w1/devices/'


def read_temp_raw(sensor):
    device_folder = glob.glob(base_dir + sensor)[0]
    device_file = device_folder + '/w1_slave'
    f = open(device_file, 'r')
    lines = f.readlines()
    f.close()
    return lines

def read_temp(sensor):
    lines = read_temp_raw(sensor)
    while lines[0].strip()[-3:] != 'YES':
        time.sleep(0.2)
        lines = read_temp_raw()
    equals_pos = lines[1].find('t=')
    if equals_pos != -1:
        temp_string = lines[1][equals_pos+2:]
        temp_c = float(temp_string) / 1000.0
        temp_f = temp_c * 9.0 / 5.0 + 32.0
        return temp_f

def log_temperature(temp, sensorId, sensorAddress, dateTime):
    print("%s %s" % (sensorAddress, temp, ))
    conn=sqlite3.connect('/home/pi/temperatureDatabase.db')
    curs=conn.cursor()

    curs.execute("INSERT INTO temperatureLog (tdatetime, sensorId, temperature) values((?), ""?"", (?))", (dateTime, sensorId,temp))
    
    conn.commit()
    conn.close()

    if sensorAddress == lower_attic_sensor:
        GPIO.output(led_list,GPIO.LOW)
        if temp >= 105:
            GPIO.output(18,GPIO.HIGH)
            GPIO.output(23,GPIO.HIGH)
        elif temp >= 100:
            GPIO.output(23,GPIO.HIGH)
        elif temp >= 95:
            GPIO.output(24,GPIO.HIGH)
            GPIO.output(25,GPIO.HIGH)
        elif temp >= 90:
            GPIO.output(25,GPIO.HIGH)
        elif temp >= 85:
            GPIO.output(12,GPIO.HIGH)
            GPIO.output(16,GPIO.HIGH)
        elif temp >= 80:
            GPIO.output(16,GPIO.HIGH)
        elif temp >= 75:
            GPIO.output(20,GPIO.HIGH)
            GPIO.output(21,GPIO.HIGH)
        else:
            GPIO.output(21,GPIO.HIGH)
        blinkcount = temp % 10
        while blinkcount > 0:
            if (blinkcount >= 5):
                GPIO.output(18, GPIO.HIGH)
                time.sleep(1)
                blinkcount = blinkcount - 5
            else:                
                GPIO.output(18, GPIO.HIGH)
                time.sleep(.1)
                blinkcount = blinkcount - 1
            GPIO.output(18, GPIO.LOW)
            time.sleep(.2)
            
            

while True:
    try:
        GPIO.output(26,GPIO.HIGH)
        currentDateTime = datetime.datetime.now()
        print(str(currentDateTime))
        for sensorId, sensorAddress in sensor_list.items():
            log_temperature(read_temp(sensorAddress), sensorId, sensorAddress, currentDateTime)
        print("__________________________________");
        GPIO.output(26,GPIO.LOW)
        time.sleep(30)
    except:
        print("error:", sys.exc_info())
        GPIO.output(18, GPIO.HIGH)
        time.sleep(3)
        GPIO.output(18, GPIO.LOW)
        time.sleep(1)
        GPIO.output(18, GPIO.HIGH)
        time.sleep(3)
        GPIO.output(18, GPIO.LOW)
        

