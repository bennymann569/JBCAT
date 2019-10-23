phonBook = {
    "Bob" : 305939,
    "Steve" : 345629349,
    "Ron Johnson" : 24444,
    "Stevie Wonder" : 666,
    "Joe Mama" : 1,
    "Brian Blessed" : 1000000,
    "Pro Jared" : 20094093405805,
    "Pro Jared's nudes" : 696969,
    "Alinity" : 0,
    "China" : 4,
    "Winnie the Pooh" : 13,
    "Ricardo" : 11,
    "Belle Delphine's bath" : 433984209358023958,
    "My wallet" : 00,
    "Kestrel" : 55555555555,
    "Q" : 7980956690,
    "QQ" : 3,
    "WWW" : 1000303030,
    "asdfmovie 11": 111111111111,
    "Tony Stark" : 545454545454545
    }

numberList = list(phonBook.values())
nameList = list(phonBook.keys())

print(numberList[::2])
print(nameList[(len(nameList)-5):])