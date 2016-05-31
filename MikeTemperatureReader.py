import os
import glob
import time
import sqlite3
import datetime

os.system('modprobe w1-gpio')
os.system('modprobe w1-therm')

sensor1 = '28-031635fb46ff'
sensor2 = '28-031636b23aff'
sensor3 = '28-0416361444ff'
sensor4 = '28-031635fff1ff'

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

def log_temperature(temp, sensor, dateTime):
    print("%s %s %s" % (sensor, temp, str(dateTime)))
    conn=sqlite3.connect('/home/pi/temperatureDatabase.db')
    curs=conn.cursor()

    curs.execute("INSERT INTO temperatureLog (tdatetime, sensorId, temperature) values((?), ""?"", (?))", (dateTime, sensor,temp))
    
    conn.commit()
    conn.close()

while True:
    currentDateTime = datetime.datetime.now()
    log_temperature(read_temp(sensor1), sensor1, currentDateTime)
    log_temperature(read_temp(sensor2), sensor2, currentDateTime)
    log_temperature(read_temp(sensor3), sensor3, currentDateTime)
    log_temperature(read_temp(sensor4), sensor4, currentDateTime)
    time.sleep(30)
