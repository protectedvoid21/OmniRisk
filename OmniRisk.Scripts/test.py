import asyncio
from pyppeteer import launch
from bs4 import BeautifulSoup
from collections import namedtuple
import string
import json

def load_text_file(filename):
    with open(filename, 'r', encoding='utf-8') as file:
        text = file.read()
    return text

async def main():
    browser = await launch()
    page = await browser.newPage()
    await page.goto('https://rps.ms.gov.pl/pl-PL/Public#/home')
    html = await page.content()
    await browser.close()
    return html

html_response = asyncio.get_event_loop().run_until_complete(main())
soup = BeautifulSoup(html_response, 'html.parser')
td_elements = soup.find_all('td', class_='ng-binding')
Person = namedtuple('Person', ['Id', 'FirstName', 'Surname', 'PhotoUrl', 'CurrentLocation'])
firstNames = []
surnames = []
photoUrl = []
currentLocations = []
images = []
for td in td_elements:
    text = td.get_text(strip=True)
    if 'Nazwisko rodowe' in text:
        surnames.append(text.split(':')[-1].strip())
    if 'Pierwsze imię' in text:
        firstNames.append(text.split(':')[-1].strip())

td_elements = soup.find_all('ins', class_='ng-binding')
for td in td_elements:
    currentLocations.append(td.get_text(strip=True))
locationDict = {
    'Nie przebywa na wolności': 0,
    'W trakcie weryfikacji': 1,
}
img_elements = soup.find_all('img')
for img in img_elements:
    images.append(img['src'])

data = []
for i in range(len(firstNames)):
    data.append({
        'Id': i,
        'FirstName': string.capwords(firstNames[i]),
        'Surname': string.capwords(surnames[i]),
        'CurrentLocation': string.capwords(currentLocations[i]) if currentLocations[i] not in ['Nie przebywa na wolności', 'W trakcie weryfikacji'] else locationDict[currentLocations[i]],
        'PhotoUrl': images[i]
    })
json_string = json.dumps(data)
text_file = open("data.txt", "w")
n = text_file.write(json_string)
text_file.close()
