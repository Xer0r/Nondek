import turtle
from turtle import *
import image_edit








size = 400
screen = turtle.Screen()
screen.screensize(size,size,)

t=Turtle()
t.width(22)
t.speed(0)
turtle_image = "C:/Users/nonde/Desktop/python_draw/paintbrush.gif"
turtle.register_shape(turtle_image)
t.shape(turtle_image)

def move(x,y):
    if abs(x-t.xcor()) >= 20 or abs(y-t.ycor()) >= 20:
        t.penup()
        t.goto(x,y)
        t.pendown()

def end():
    turtle.bye()

def erase():
    t.clear()

def takepic():
    t.getscreen().getcanvas().postscript(file='myImage.ps')
    image_edit.Recognise_image()
    erase()

def dragging(x,y):
    t.ondrag(None)
    if abs(x) <= 250 and abs(y) <= 250:
        t.goto(x,y)
        t.pendown()
    else: 
        t.penup()
    t.ondrag(dragging) 

def main():
    turtle.listen()
    screen.listen()
    t.ondrag(dragging)
    screen.onscreenclick(move)
    screen.onkey(takepic, "Return")
    screen.onkey(erase, "BackSpace")
    screen.onkey(end, "Escape")  

screen.mainloop
if __name__ == '__main__':
    main()
turtle.done()