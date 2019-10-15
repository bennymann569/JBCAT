def Main():

    fullArray = [None]

    #We load up the Array of all the command entries here, making sure parents are linked to children
    def InitArray():
        #For DataEntry in (wherever we put all the info), fullArray.Add(DataEntry)