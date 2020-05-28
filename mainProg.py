##########################################################
#####      Imports                       #################
##########################################################
import tkinter as tk
from tkinter import filedialog
from xlsxwriter import Workbook
from tkinter import messagebox
from selenium import webdriver as wb
from selenium.common import exceptions
import xlrd
import openpyxl
from openpyxl.styles import PatternFill
import csv
import time
import os
import shutil
import getpass

##########################################################
#####      Constants                     #################
##########################################################

REVENUE = 3
EPS = 8
BOOKVALUE = 12
FREECASHFLOW = 15
OPERATINGCASH = 13
ROIC = 38
dsglob = []
numbers = {}
DataFileName = ".data"
numberOfSettingBrowse = 2
settingBrowseNames = ["filesLocation","favStocksLocation"]
mainFilePath = "D:/Rule1/inv.xlsx" # need to be changed
CurrentAAABondYield = 2.41



###########################################################
###############        Page Class         #################
###########################################################
class Page(tk.Frame):
    def __init__(self,*args,**kwargs):
        tk.Frame.__init__(self,*args,**kwargs)
        self.rowconfigure(0, weight=1)
        self.rowconfigure(1, weight=18)
        self.rowconfigure(2, weight=1)
        self.grid_columnconfigure(0,weight=1)
        self.header = tk.Frame(self)
        self.content = tk.Frame(self)
        self.footer = tk.Frame(self)
        head = tk.Label(self.header, text="StockAnalysis")
        foot = tk.Label(self.footer, text="Copyright BS", anchor='s')
        head.pack()
        foot.pack()
        self.header.grid(row=0)
        self.content.grid(row=1)
        self.footer.grid(row=2)
    def show(self):
        self.pack(fill=tk.BOTH, expand=1)
    def hide(self):
        self.pack_forget()

##########################################################
#####      Frames and Widgets:           #################
##########################################################

if(os.path.isfile(DataFileName)==False):
    f = open(DataFileName,'w+')
    f.close()
root = tk.Tk()
#root.title('Title')
#root.iconbitmap('pathToImage')
root.geometry('1200x600')
root.grid_columnconfigure(0,weight=1)

##########################################################
#####      PagesDeclarations             #################
##########################################################

mainPage = Page(root)
stockDataPage = Page(root)
calValuePage = Page(root)
settingsPage = Page(root)
aboutPage = Page(root)

######################################################################################
###############                 functions:                           #################
######################################################################################

def createDataFile():
    f = open(DataFileName, 'w+')
    f.close()

def rule1Calculator(epsGrowth,PE,currentEPS):
    if(epsGrowth<0 or PE<0 or currentEPS<0):
        return 0
    return PE*currentEPS*pow(epsGrowth,10)/4

def benGrahamOriginal(epsGrowth,currentEPS):
    if (epsGrowth < 0 or currentEPS < 0):
        return 0
    return (currentEPS * (8.5 + 2 * epsGrowth) * 4.4) / CurrentAAABondYield

def benGrahamUpdate(epsGrowth,currentEPS):
    if(epsGrowth<0 or currentEPS < 0):
        return 0
    return (currentEPS*(7+epsGrowth)*4.4)/CurrentAAABondYield

def getDataLocation():
    if(os.path.isfile(DataFileName)==False):
        createDataFile()
        return None
    ans = {settingBrowseNames[0]:'',settingBrowseNames[1]:''}
    i = 0
    dataFile = open(DataFileName,'r')
    for line in dataFile:
        ans[settingBrowseNames[i]] = line.rstrip()
        i += 1
        if i>= numberOfSettingBrowse:
            break
    dataFile.close()
    return ans

def isDataLocValid():
    dataFile = open(DataFileName,'r')
    for line in dataFile:
        if(os.path.isdir(line.rstrip())):
            continue
        return False
    return True

def writeFromData():
    """ a function that print to the entry the locations that are in the .data file"""
    loc = getDataLocation()
    if(loc==None):
        return
    if(len(loc) == numberOfSettingBrowse):
        stockFilesEntry.delete(0,'end')
        stockFilesEntry.insert(0, loc[settingBrowseNames[0]])
        favStocksEntry.delete(0,'end')
        favStocksEntry.insert(0, loc[settingBrowseNames[1]])
    elif(len(loc)==1):
        stockFilesEntry.delete(0, 'end')
        stockFilesEntry.insert(0, loc[settingBrowseNames[0]])


def moveLocation(oldLoc,newLoc):
    src = oldLoc[1]+"inv.xlsx"
    dest = newLoc[1]+"inv.xlsx"
    if(os.path.isfile(src)==True and os.path.isfile(dest)==False):
        shutil.move(src,dest)



def saveLocToDataFile(locList):
    for i,loc in enumerate(locList,1):
        if((not os.path.isdir(loc.rstrip())) and not loc.rstrip()==""):
            raise OSError("directory of the entry number " + str(i) + " does not exist")
    fileToWrite = open(DataFileName,'w')
    for loc in locList:
        loc = loc.rstrip() + '\n'
        fileToWrite.write(loc)
    fileToWrite.close()

def saveLocToDataFileWrapper(locList):
    try:
        saveLocToDataFile(locList)
    except OSError as err:
        messagebox.showerror("ERROR",err)
        return
    saveButton["state"]=tk.DISABLED

def browseLocation(typeOfData):
    """ a function that write to the .data file the locations of the files"""
    dir = filedialog.askdirectory()
    if (not dir == ''):
        dir += '\n'
    else:
        return
    if(os.path.isfile(DataFileName) == False):
        f = open(DataFileName, 'w+')
        f.write(dir)
    else:
        fileToRead = open(DataFileName,'r')
        numOfLines = 0
        for l in fileToRead:
            numOfLines = numOfLines+1
        lines = []
        fileToRead.close()
        fileToRead = open(DataFileName,'r')
        for line in fileToRead:
            lines.append(line)
        fileToRead.close()
        newLines = []
        for count in range(numOfLines):
            newLines.append(lines[count])
        for count in range(numOfLines,numberOfSettingBrowse):
            newLines.append("\n")
        if(typeOfData==settingBrowseNames[0]):
            newLines[0] = dir
        elif(typeOfData==settingBrowseNames[1]):
            newLines[1] = dir
        try:
            saveLocToDataFile(newLines)
        except OSError as err:
            messagebox.showerror("ERROR", err)
            return
        if(numOfLines>=2):
            moveLocation(lines,newLines)
    writeFromData()



def switchFrames(src,dest):
    src.hide()
    dest.show()

def checkCalculatorInput(inputs):
    for inp in inputs:
        try:
            float(inp)
        except:
            return False;
    return True;

def onFocusEntry(event):
    """a function that gets called whenever entry is clicked"""
    if event.widget.cget('fg')=='grey':
        event.widget.delete(0,"end")
        event.widget.insert(0,'')
        event.widget.config(fg='black')

def onFocusOut(event,msg):
    if(event.widget.get()==''):
        event.widget.insert(0,msg)
        event.widget.config(fg='grey')

def onChangeLoc(event):
    ans = getDataLocation()
    isChanged = False
    if not ans[settingBrowseNames[0]]==stockFilesEntry.get():
        isChanged=True
    if not ans[settingBrowseNames[1]]==favStocksEntry.get():
        isChanged=True
    if isChanged:
        saveButton["state"]=tk.NORMAL

def isCellEmpty(sheet,row,col):
    return sheet.cell_value(row,col) == ""

def isCellNegative(sheet,row,col):
    return float(sheet.cell_value(row,col)) <= 0

def checkWhichEmpty(sheet,row):
    empty = []
    for i in range(1,11):
        if isCellEmpty(sheet,row,i):
            empty.append(i)
    return empty

def checkWhichNegative(sheet,row):
    negative = []
    for i in range(1,11):
        if isCellNegative(sheet,row,i):
            negative.append(i)
    return negative

def createList(sheet,row):
    numList = []
    empties = checkWhichEmpty(sheet,row)
    negatives = checkWhichNegative(sheet,row)
    if isCellEmpty(sheet,row,2):
        return "Not enough Data!!"
    if not isCellEmpty(sheet,row,1):
        if not isCellNegative(sheet,row,1):
            numList.append(sheet.cell_value(row,1))
        elif isCellNegative(sheet,row,2):
            return "Not enough Data!!"
        else:
            numList.append(sheet.cell_value(row,2))
    for cell in range(10):
        numList.append(sheet.cell_value(row, cell+2))
    return numList

def calGrowth(start,end,years,rnd):
    if(start<0):
        return 9.99
    return round((pow(end/start,1/years)-1),rnd)

def findrow(filePath,stock):
    wb = xlrd.open_workbook(filePath)
    sheet = wb.sheet_by_index(0)
    i = 0
    try:
        for i in range(300):
            if not sheet.cell(i+16,1) or sheet.cell_value(i+16,1) == stock:
                return i+16
    except:
        return i+16

def calAverage(numbers,rnd):
    sum = 0
    for num in numbers:
        sum += num
    return round((sum/len(numbers))/100,rnd)

def saveStock(filePath, symbol, num, color):
    mainWB = openpyxl.load_workbook(filePath)
    ws = mainWB.active
    saveRow = findrow(filePath,symbol.upper())+1
    redFill = PatternFill(start_color="FF0000", end_color="FF0000", fill_type="solid")
    greenFill = PatternFill(start_color="00FF00", end_color="00FF00", fill_type="solid")
    if color == "R":
        ws.cell(column=2,row=saveRow,value=symbol).fill = redFill
    else:
        ws.cell(column=2, row=saveRow, value=symbol).fill = greenFill
    for i in range(len(num)):
        for j in range(len(num["ROIC"])):
            ws.cell(column=i*3+j+3, row=saveRow, value=list(num[list(num.keys())[i]].values())[j])
    mainWB.save(filePath)

def isValidChoice(choice,ra):
    for num in range(ra):
        if choice==num+1:
            return True
    return False

def isListsValid(lt):
    for l in lt:
        if l=="Not enough Data!!":
            return False
    return True


def calNumbers(data,avgOrGrowth):
    dic = {}
    ROUND = 4
    if avgOrGrowth=="GROWTH":
        dic["ten"] = calGrowth(data[0], data[-2], len(data) - 2, ROUND)
        dic["five"] = calGrowth(data[-7], data[-2], 5, ROUND)
        dic["one"] = calGrowth(data[-3], data[-2], 1, ROUND)
    elif avgOrGrowth=="AVERAGE":
        dic["ten"] = calAverage(data[:-1], ROUND)
        dic["five"] = calAverage(data[-6:-1], ROUND)
        dic["one"] = calAverage(data[-2:-1], ROUND)
    return dic

def printPriceAndMos(prices):
    for i in range(6):
        if i%2==1:
            prices[i] = "MOS Price: " + str(prices[i])
        else:
            prices[i] = "Intrinsic Value: " + str(prices[i])
    rule1Header = tk.Label(calValuePage.content,text="Prices by Rule 1:")
    ben1Header = tk.Label(calValuePage.content,text="Prices by Ben Original:")
    ben2Header = tk.Label(calValuePage.content,text="Prices by Ben Update:")
    labelRule1Price = tk.Label(calValuePage.content,text=prices[0])
    labelRule1Mos = tk.Label(calValuePage.content,text=prices[1])
    labelBenOriginalPrice = tk.Label(calValuePage.content,text=prices[2])
    labelBenOriginalMos = tk.Label(calValuePage.content,text=prices[3])
    labelBenUpdatePrice = tk.Label(calValuePage.content,text=prices[4])
    labelBenUpdateMos = tk.Label(calValuePage.content,text=prices[5])
    rule1Header.grid(row="4", column="0")
    ben1Header.grid(row="6",column="0")
    ben2Header.grid(row="8",column="0")
    labelRule1Price.grid(row="5",column="0")
    labelRule1Mos.grid(row="5",column="1")
    labelBenOriginalPrice.grid(row="7",column="0")
    labelBenOriginalMos.grid(row="7",column="1")
    labelBenUpdatePrice.grid(row="9",column="0")
    labelBenUpdateMos.grid(row="9",column="1")


# def calculateValue():
#     currentEPS = float(input("Enter current EPS: "))
#     EPSGrowth = float(input("Enter EPS growth (as percentage): "))
#     PE = float(input("Enter PE: "))
#     EPSGrowth = (EPSGrowth / 100) + 1
#     IV = PE * currentEPS * pow(EPSGrowth, 10) / 4
#     MOS = IV / 2
#     print("The Intrinsic Value is: " + str(IV))
#     print("The MOS Price is: " + str(MOS))




def getBig5Numbers(stock):
    if(not isDataLocValid()):
        messagebox.showerror("Error","Please enter valid locations inside the setting page.")
        return
    isDownload = messagebox.askyesno("fromOnline", "Do you want to download new data?", )
    for b in dsglob:
        b.destroy()
    if(isDownload==True):
        result = downloadStockData(stock)
        if(result==-1):
            return -1
        changePlaceForTheFile(stock)
        convert_CSV_To_XLSX(stock)
    loc = getDataLocation()
    fileName = loc[settingBrowseNames[0]] + stock + " Key Ratios.xlsx"
    try:
        wb = xlrd.open_workbook(fileName)
    except FileNotFoundError:
        messagebox.showerror("ERROR","File with the stock data not found")
        return
    sheet = wb.sheet_by_index(0)
    revenue = createList(sheet, REVENUE)
    eps = createList(sheet, EPS)
    equity = createList(sheet, BOOKVALUE)
    freeCashFlow = createList(sheet, FREECASHFLOW)
    operatingCashFlow = createList(sheet, OPERATINGCASH)
    roic = createList(sheet, ROIC)
    if not isListsValid([revenue, eps, equity, freeCashFlow, operatingCashFlow, roic]):
        lable = tk.Label(stockDataPage.content,text="Not Enough Data to Calculate")
        lable.grid(row=1,column=0)
        dsglob.append(lable)
        return 0
    numbers["ROIC"] = calNumbers(roic, "AVERAGE")
    numbers["Equity"] = calNumbers(equity, "GROWTH")
    numbers["EPS"] = calNumbers(eps, "GROWTH")
    numbers["Revenue"] = calNumbers(revenue, "GROWTH")
    numbers["FreeCashFlow"] = calNumbers(freeCashFlow, "GROWTH")
    numbers["OperatingCashFlow"] = calNumbers(operatingCashFlow, "GROWTH")
    i1 = 0
    j1 = 0
    for key in numbers.keys():
        tLabel = tk.Label(stockDataPage.content, text=key + ":")
        tLabel.grid(row=1,column=i1%6)
        dsglob.append(tLabel)
        for key2 in numbers[key]:
            t2Label = tk.Label(stockDataPage.content,text=key2 + ": " + str(round((numbers[key])[key2]*100,4)))
            t2Label.grid(row=2+j1%3,column=i1%6)
            dsglob.append(t2Label)
            j1 = j1+1
        i1 = i1+1
    result = messagebox.askyesno("SaveData","Do you want to save the stock?")
    if(result==True):
        res2 = messagebox.askyesno("SaveData","Do you want to save as green?",)
        res3 = messagebox.askyesno("SaveData","Do you want to save the cash?")
        if(res3==True):
            del numbers["OperatingCashFlow"]
        else:
            del numbers["FreeCashFlow"]
        if (res2 == True):
            saveStock(mainFilePath, stock, numbers, "GREEN")
        else:
            saveStock(mainFilePath, stock, numbers, "RED")

    return 1



def convert_CSV_To_XLSX(stock):
    oldname = stock + " Key Ratios.csv"
    oldPath = "D:/StockXLSXFolder/" + oldname
    newname = stock + " Key Ratios.xlsx"
    newPath = "D:/StockXLSXFolder/" + newname
    if (os.path.isfile(newPath)==True):
        os.remove(newPath)
    wbook = Workbook(newPath)
    sheet1 = wbook.add_worksheet()
    count = 0
    with open(oldPath, 'rt') as f:
        data = csv.reader(f)
        for row in data:
            if (len(row) <= 0):
                count = count + 1
                continue
            if row[-1] != "TTM" and not row[-1].startswith("Latest"):
                for i in range(len(row)):
                    if i > 0 and row[i] != "":
                        row[i] = row[i].replace(",", "")
                        row[i] = float(row[i])
            sheet1.write_row(count, 0, row)
            count = count + 1
    wbook.close()


def downloadStockData(stock):
    drive = wb.Chrome()
    stock = stock.upper()
    drive.get("https://financials.morningstar.com/ratios/r.html?t=" + stock)
    try:
        drive.find_element_by_css_selector('.large_button').click()
    except exceptions.NoSuchElementException:
        messagebox.showerror("ERROR!","No Such Stock Exists")
        return -1
    time.sleep(3)
    drive.quit()
    return 1

def changePlaceForTheFile(stock):
    filename = stock + " Key Ratios.csv"
    src = "C:/Users/BenGabay/Downloads/" + filename
    dest = "D:/StockXLSXFolder/" + filename
    shutil.move(src,dest)


def calVal(epsGrowth,PE,currentEPS):
    isGood = checkCalculatorInput({epsGrowth,PE,currentEPS})
    if not isGood:
        messagebox.showerror("Erron","Invalid entries, Please insert valid numbers")
        return
    epsGrowth = (float(epsGrowth)/100)+1
    PE = float(PE)
    currentEPS = float(currentEPS)
    rule1Price = rule1Calculator(epsGrowth,PE,currentEPS)
    rule1Mos = rule1Price/2
    ben1Price = benGrahamOriginal(epsGrowth,currentEPS)
    ben1Mos = ben1Price/2
    ben2Price = benGrahamUpdate(epsGrowth,currentEPS)
    ben2Mos = ben2Price/2
    prices= [rule1Price,rule1Mos,ben1Price,ben1Mos,ben2Price,ben2Mos]
    printPriceAndMos(prices)


    # currentEPS = float(input("Enter current EPS: "))
    # EPSGrowth = float(input("Enter EPS growth (as percentage): "))
    # PE = float(input("Enter PE: "))
    # EPSGrowth = (EPSGrowth / 100) + 1
    # IV = PE * currentEPS * pow(EPSGrowth, 10) / 4
    # MOS = IV / 2
    # print("The Intrinsic Value is: " + str(IV))
    # print("The MOS Price is: " + str(MOS))





###########################################################
###############        mainPage           #################
###########################################################

option1 = tk.Button(mainPage.content, text="Calculate Stock Data", command=lambda:switchFrames(mainPage,stockDataPage))
option2 = tk.Button(mainPage.content, text="Calculate intrinsic value", command=lambda:switchFrames(mainPage,calValuePage))
option3 = tk.Button(mainPage.content,text="Settings", command=lambda:switchFrames(mainPage,settingsPage))
option4 = tk.Button(mainPage.content, text="About", command=lambda:switchFrames(mainPage,aboutPage))
option5 = tk.Button(mainPage.content, text="Exit", command=root.quit)
option1.grid(row=0,column=0,padx=10,pady=10)
option2.grid(row=1, column=0, padx=10, pady=10)
option3.grid(row=2, column=0, padx=10, pady=10)
option4.grid(row=3, column=0, padx=10, pady=10)
option5.grid(row=4, column=0, padx=10, pady=10)
mainPage.show()



###########################################################
###############        StockDataPage      #################
###########################################################

dataButton = tk.Button(stockDataPage.content, text="Get Stock Data", command=lambda: getBig5Numbers(symbol.get()))
back = tk.Button(stockDataPage.content, text="Back",command=lambda:switchFrames(stockDataPage,mainPage))
symbol = tk.Entry(stockDataPage.content, width=20, borderwidth=5)
symbol.bind('<FocusIn>', onFocusEntry)
symbol.bind('<FocusOut>', lambda event, param="Enter Symbol": onFocusOut(event,param))
symbol.grid(row=0,column=0)
dataButton.grid(row=0,column=1)
back.grid(row=100,column=0,pady=10)

###########################################################
###############     calValuePage          #################
###########################################################

back = tk.Button(calValuePage.content, text="Back", command=lambda: switchFrames(calValuePage,mainPage))
entryValEps = tk.Entry(calValuePage.content, width=30, borderwidth=5)
entryValEps.bind('<FocusIn>',onFocusEntry)
entryValEps.bind('<FocusOut>',lambda event, param="EPS...": onFocusOut(event,param))
entryValEps.grid(row=0, column=1)
labelValEps = tk.Label(calValuePage.content, text="Current EPS: ")
labelValEps.grid(row=0, column=0)
entryGrowthEps = tk.Entry(calValuePage.content, width=30, borderwidth=5)
entryGrowthEps.bind('<FocusIn>',onFocusEntry)
entryGrowthEps.bind('<FocusOut>',lambda event, param="EPS Growth...": onFocusOut(event,param))
entryGrowthEps.grid(row=1, column=1)
labelGrowthEps = tk.Label(calValuePage.content, text="EPS Growth: ")
labelGrowthEps.grid(row=1, column=0)
entryPe = tk.Entry(calValuePage.content, width=30, borderwidth=5)
entryPe.bind('<FocusIn>',onFocusEntry)
entryPe.bind('<FocusOut>',lambda event, param="PE...": onFocusOut(event,param))
entryPe.grid(row=2, column=1)
labelPe = tk.Label(calValuePage.content, text="Forword PE: ")
labelPe.grid(row=2, column=0)
bCal = tk.Button(calValuePage.content, text="Calculate Value",command=lambda: calVal(entryGrowthEps.get(),entryPe.get(),entryValEps.get()))
bCal.grid(row=3, column=0, pady=5)
back.grid(row=100, column=0)


###########################################################
###############     SettingPage           #################
###########################################################

backSetting = tk.Button(settingsPage.content, text="Back", command=lambda: switchFrames(settingsPage,mainPage))
stockFilesEntry = tk.Entry(settingsPage.content, width="60", borderwidth="5")
stockFileLabel = tk.Label(settingsPage.content, text="stock data files location: ")
stockFilesButton = tk.Button(settingsPage.content, text="browse", command=lambda: browseLocation(settingBrowseNames[0]))
favStocksEntry = tk.Entry(settingsPage.content, width="60", borderwidth="5")
favStocksLabel = tk.Label(settingsPage.content, text="favorite stocks location: ")
favStocksButton = tk.Button(settingsPage.content, text="browse", command=lambda: browseLocation(settingBrowseNames[1]))
saveButton = tk.Button(settingsPage.content, text="save",command=lambda: saveLocToDataFileWrapper([stockFilesEntry.get(),favStocksEntry.get()]),state=tk.DISABLED)
writeFromData()
stockFilesEntry.bind('<KeyPress>',onChangeLoc)
favStocksEntry.bind('<KeyPress>',onChangeLoc)
stockFileLabel.grid(row=0,column=0)
stockFilesEntry.grid(row=0,column=1)
stockFilesButton.grid(row=0,column=2)
favStocksLabel.grid(row=1,column=0)
favStocksEntry.grid(row=1,column=1)
favStocksButton.grid(row=1,column=2)
backSetting.grid(row=100, column=0)
saveButton.grid(row=100, column=1)
# TODO: Build The setting page
# TODO: If he change location then I should move the files to the new location.

###########################################################
###############     AboutPage             #################
###########################################################

# TODO: Build the AboutPage

##########################################################
#####      main program:                 #################
##########################################################


root.mainloop()