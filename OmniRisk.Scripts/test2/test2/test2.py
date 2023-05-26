from selenium import webdriver
PATH = "C:\Program Files (x86)\geckodriver.exe"
driver = webdriver.Firefox(PATH)

driver.get("https://rps.ms.gov.pl/pl-PL/Public#/home")
driver.quit()
