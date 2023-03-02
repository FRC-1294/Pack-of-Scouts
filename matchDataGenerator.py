import requests

request_data = requests.get("https://www.thebluealliance.com/api/v3/event/(EVENT_ID)/matches/simple?X-TBA-Auth-Key=(API_KEY)")

request_data_json = request_data.json()   
for match in request_data_json:
    team_numbers = [-1,-1,-1,-1,-1,-1]
    team_numbers[0] = match['alliances']['red']['team_keys'][0][3:]
    team_numbers[1] = match['alliances']['red']['team_keys'][1][3:]
    team_numbers[2] = match['alliances']['red']['team_keys'][2][3:]
    team_numbers[3] = match['alliances']['blue']['team_keys'][0][3:]
    team_numbers[4] = match['alliances']['blue']['team_keys'][1][3:]
    team_numbers[5] = match['alliances']['blue']['team_keys'][2][3:]

    if match['comp_level'] != "qm":
        continue
   
    match_number = str(match['match_number'])
    
    f = open("match_schedule.txt",  "a")
    f.write(match_number + " " + team_numbers[0] + " " + team_numbers[1] + " " + team_numbers[2] + " " + team_numbers[3] + " " + team_numbers[4] + " " + team_numbers[5] + "\n")
    f.close()



