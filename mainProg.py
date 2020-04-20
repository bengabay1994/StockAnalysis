import tkinter as tk
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

##########################################################
#####      Constants         :           #################
##########################################################

REVENUE = 3
EPS = 8
BOOKVALUE = 12
FREECASHFLOW = 15
OPERATINGCASH = 13
ROIC = 38
dsglob = []
numbers = {}
mainFilePath = "D:/Rule1/inv.xlsx" # need to be changed



###########################################################
###############        Page Class      #################
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

def findXlsFilesLocation():
    # TODO: Add Your Code Here.
    return 0

def findDownloadFilesLocation():
    # TODO: Add Your Code Here.
    return 0

def switchFrames(src,dest):
    src.hide()
    dest.show()




def onFocusEntry(event, entry):
    """a function that gets called whenever entry is clicked"""
    if entry.cget('fg')=='grey':
        entry.delete(0,"end")
        entry.insert(0,'')
        entry.config(fg='black')
def onFocusOut(event,entry,msg):
    if(entry.get()==''):
        entry.insert(0, msg)
        entry.config(fg=''grey)


def isCellEmpty(sheet,row,col):
    return sheet.cell_value(row,col) == ""

def isCellNegative(sheet,row,col):
    return float(sheet.cell_value(row,col)) <= 0

def createList(sheet,row):
    numList = []
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
    isDownload = messagebox.askyesno("fromOnline", "Do you want to download new data?", )
    for b in dsglob:
        b.destroy()
    if(isDownload==True):
        result = downloadStockData(stock)
        if(result==-1):
            return -1
        changePlaceForTheFile(stock)
        convert_CSV_To_XLSX(stock)
    fileName = "D:/StockXLSXFolder/" + stock + " Key Ratios.xlsx"
    wb = xlrd.open_workbook(fileName)
    sheet = wb.sheet_by_index(0)
    revenue = createList(sheet, REVENUE)
    eps = createList(sheet, EPS)
    equity = createList(sheet, BOOKVALUE)
    freeCashFlow = createList(sheet, FREECASHFLOW)
    operatingCashFlow = createList(sheet, OPERATINGCASH)
    roic = createList(sheet, ROIC)
    if not isListsValid([revenue, eps, equity, freeCashFlow, operatingCashFlow, roic]):
        lable = tk.Label(content,text="Not Enough Data to Calculate")
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
        tLabel = tk.Label(content, text=key + ":")
        tLabel.grid(row=1,column=i1%6)
        dsglob.append(tLabel)
        for key2 in numbers[key]:
            t2Label = tk.Label(content,text=key2 + ": " + str(round((numbers[key])[key2]*100,4)))
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


def intrinsicCal(ds):
    for b in ds:
        b.destroy()
    back = tk.Button(content, text="Back", command=lambda: mainView([back,tEPS,tEPSGrowth,lEPS,lEPSGrowth,tPE,lPE,bCal]))
    back.grid(row=4,column=0)
    tEPS = tk.Entry(content, width=30, borderwidth=5)
    tEPS.insert(0, "Current EPS")
    tEPS.grid(row=0, column=1)
    lEPS = tk.Label(content,text="Current EPS: ")
    lEPS.grid(row=0,column=0)
    tEPSGrowth = tk.Entry(content, width=30, borderwidth=5)
    tEPSGrowth.insert(0, "EPS growth(as percentage)")
    tEPSGrowth.grid(row=1,column=1)
    lEPSGrowth = tk.Label(content, text="EPS Growth: ")
    lEPSGrowth.grid(row=1,column=0)
    tPE = tk.Entry(content, width=30, borderwidth=5)
    tPE.insert(0, "PE")
    tPE.grid(row=2, column=1)
    lPE = tk.Label(content, text="PE: ")
    lPE.grid(row=2, column=0)
    bCal = tk.Button(content, text="Calculate Value", command=lambda: calVal((float(tEPSGrowth.get())/100)+1,float(tPE.get()),float(tEPS.get())))
    bCal.grid(row=3,column=0, pady=5)

def calVal(epsGrowth,PE,currentEPS): # it comes as str fix it ben!!!
    IV = PE * currentEPS * pow(epsGrowth, 10) / 4
    MOS = IV/2
    ivLabel = tk.Label(content,text="The intrinsic Value is: " + str(IV))
    mosLabel = tk.Label(content, text="the MOS Price is: " + str(MOS))
    ivLabel.grid(row=3,column=1)
    mosLabel.grid(row=4,column=1)
    dsglob.append(ivLabel)
    dsglob.append(mosLabel)


    # currentEPS = float(input("Enter current EPS: "))
    # EPSGrowth = float(input("Enter EPS growth (as percentage): "))
    # PE = float(input("Enter PE: "))
    # EPSGrowth = (EPSGrowth / 100) + 1
    # IV = PE * currentEPS * pow(EPSGrowth, 10) / 4
    # MOS = IV / 2
    # print("The Intrinsic Value is: " + str(IV))
    # print("The MOS Price is: " + str(MOS))


###########################################################
###############     mainPage              #################
###########################################################


def stockData(ds):
    for b in ds:
        b.destroy()
    dataButton = tk.Button(content, text="Get Stock Data", command=lambda: getBig5Numbers(symbol.get()))
    back = tk.Button(content, text="Back",command=lambda: mainView([dataButton,back,symbol]))
    symbol = tk.Entry(content, width=20, borderwidth=5)
    symbol.insert(0, "Enter Symbol")
    symbol.grid(row=0, column=0)
    dataButton.grid(row=0, column=1)
    back.grid(row=100,column=0,pady=10)



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



###########################################################
###############        StockDataPage      #################
###########################################################

dataButton = tk.Button(stockDataPage.content, text="Get Stock Data", command=lambda: getBig5Numbers(symbol.get()))
back = tk.Button(stockDataPage.content, text="Back",command=lambda:switchFrames(stockDataPage,mainPage))
symbol = tk.Entry(stockDataPage.content, width=20, borderwidth=5)
symbol.bind('<FocusIn>', onFocusEntry(symbol))
symbol.bind('<FocusOut>', onFocusOut(symbol,"Enter Symbol"))
symbol.grid(row=0,column=0)
dataButton.grid(row=0,column=1)
back.grid(row=100,column=0,pady=10)

###########################################################
###############     calValuePage          #################
###########################################################



###########################################################
###############     SettingPage           #################
###########################################################



###########################################################
###############     AboutPage             #################
###########################################################


##########################################################
#####      main program:                 #################
##########################################################

root.mainloop()