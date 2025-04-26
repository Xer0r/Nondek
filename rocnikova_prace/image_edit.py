import numpy as np
import cv2
from PIL import ImageChops, Image
from turtle import *
from resizeimage import resizeimage
import neural_network


count = 0
def Recognise_image():
    global count
    count += 1
    psimage = Image.open('myImage.ps')
    psimage.save('myImage.png')
    picture = Image.open('myImage.png')
    picture = picture.convert('L')
    picture.save('myImage2.png')

    picture = Trim_image(picture)

    picture.save('myImage3.png')
    picture = resizeimage.resize_width(picture, 20)
    picture.save(f"digits/digit{count}.png")
    picture.save('myImage4.png')
    img = cv2.imread('digits/digit{}.png'.format(count))[:,:,0]
    img= cv2.copyMakeBorder(img, 4, 4, 4, 4, cv2.BORDER_CONSTANT, value = [255,255,255])
    Image.fromarray(img).save('myImage5.png')
    img = np.invert(img)
    Image.fromarray(img).save('myImage6.png')
    image_arr = np.array(img).astype("float32") / 255.0
    image_arr = np.expand_dims(image_arr, 0)
    image_arr = np.expand_dims(image_arr, -1)
    neural_network.Recognise(image_arr)

    

def Trim_image(im):
    bg = Image.new(im.mode, im.size, (255))
    diff = ImageChops.difference(im, bg)
   # diff = ImageChops.add(diff, diff, 2.0, -100)
    cropbox = diff.getbbox()
    if cropbox:
        x_resize_factor = cropbox[2]-cropbox[0]
        y_resize_factor = cropbox[3]-cropbox[1]

        if y_resize_factor > x_resize_factor:
            cropbox = (cropbox[0] - (y_resize_factor-x_resize_factor)/2, cropbox[1], cropbox[2] + (y_resize_factor-x_resize_factor)/2, cropbox[3])

        elif x_resize_factor > y_resize_factor:
            cropbox = (cropbox[0], cropbox[1] - (x_resize_factor-y_resize_factor)/2, cropbox[2], cropbox[3] + (x_resize_factor-y_resize_factor)/2)
        
    return im.crop(cropbox)