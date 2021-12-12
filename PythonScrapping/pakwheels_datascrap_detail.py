# modules import section
from selenium import webdriver
from selenium.webdriver.support import wait
from selenium.webdriver.support.ui import WebDriverWait
from selenium.common.exceptions import NoSuchElementException

from bs4 import BeautifulSoup

import requests

import json

# reading data from json file
adsData = []
with open('data.json', 'r') as file:
    adsData = json.load(file)

print('Lenght Before Details Scrapping: ' + str(len(adsData)))

driver = webdriver.Chrome('G:\My Drive\Assignments\InformationProcessingTechniques\IPT Course Project\PythonScrapping\chromedriver.exe')
for i in range (10):
    ad = adsData[i]
    adUrl = ad['offers']['url']
    driver.get(adUrl)
    try: 
        divTag = driver.find_element_by_class_name("ad-listing-template")
        print(divTag)
        jsonDataDetails = divTag.find_element_by_tag_name('script').get_attribute('innerHTML')

        try:
            featuresList = []
            features = driver.find_element_by_class_name('car-feature-list').find_elements_by_tag_name('li')
            for feature in features:
                featuresList.append(feature.text)
                
            # print(jsonData)
            ad['adUrlScrapping'] = {
                'details': json.loads(jsonDataDetails),
                'features': featuresList
            }
        except NoSuchElementException:
            ad['adUrlScrapping'] = {
                'details': json.loads(jsonDataDetails)
            }
    # print(ad)
    except NoSuchElementException:
        continue

    adsData[i] = ad
    # input('press any key to continue...')

print('Lenght After Details Scrapping: ' + str(len(adsData)))
with open('data1.json', 'w') as file:
    json.dump(adsData, file)
