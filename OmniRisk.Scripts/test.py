from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.by import By
import time
PATH = '.'
driver = webdriver.Edge(PATH)

driver.get('https://rps.ms.gov.pl/pl-PL/Public#/home')

strona = driver.find_element(By.TAG_NAME, "tbody")

pedofile = strona.find_elements(By.XPATH, "*")

for pedofil in pedofile:
    wPedofilu = pedofil.find_element(By.TAG_NAME, "td")

    dane = wPedofilu.find_elements(By.XPATH, "*")

    wDane2 = dane[2].find_element(By.XPATH, "*")

    wWDane2 = wDane2.find_elements(By.XPATH, "*")

    wDaneTable = wWDane2[0].find_element(By.TAG_NAME, "table")

    wDaneTbody = wDaneTable.find_element(By.TAG_NAME, "tbody")

    danePedofila = wDaneTbody.find_elements(By.XPATH, "*")

    for daneDane in danePedofila:
        daneDaneDane = daneDane.find_elements(By.XPATH, "*")
        print(daneDaneDane[0].text)


time.sleep(20)

driver.quit()
