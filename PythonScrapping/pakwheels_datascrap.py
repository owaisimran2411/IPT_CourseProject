# modules import section
from selenium import webdriver
from selenium.webdriver.support import wait
from selenium.webdriver.support.ui import WebDriverWait
from selenium.common.exceptions import NoSuchElementException

from bs4 import BeautifulSoup

import requests

import json
# import pandas as pd

# initiate chrome driver
driver = webdriver.Chrome('G:\My Drive\Assignments\InformationProcessingTechniques\IPT Course Project\PythonScrapping\chromedriver.exe')

adsData = []
# with open('data.json') as file:
#     adsData = json.load(file)



for i in range(4,8):
# accessing pakwheels website

    driver.get('https://www.pakwheels.com/used-cars/search/-/?page=' + str(i))

    # getting the main <ul> tag for getting the listing
    headUlTags = driver.find_elements_by_xpath("//*[@class='list-unstyled search-results search-results-mid next-prev car-search-results ']")


    for ul in headUlTags:
        nestedTags = ul.find_elements_by_class_name("classified-listing")
        print(len(nestedTags))
        for li in nestedTags:
            # print(dir(li))
            jsonData = li.find_element_by_tag_name('script')
            jsonScript = jsonData.get_attribute('innerHTML')
            jsonDataConverted = json.loads(jsonScript)
            try:
                print(jsonDataConverted['offers'])
                adsData.append(jsonDataConverted)
            except NoSuchElementException:
                print('<li> tag does not contain script tag')



print(len(adsData))
with open('data.json', 'w') as file:
    json.dump(adsData, file)





# stall before closing driver
# input('press any key to continue ....')
