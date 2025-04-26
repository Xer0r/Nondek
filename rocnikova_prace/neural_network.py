import tensorflow as tf
import keras
from keras import layers
import matplotlib.pyplot as plt
import numpy as np
import random

(x_train, y_train), (x_test, y_test) = keras.datasets.mnist.load_data()


x_train = x_train.astype("float32") / 255.0
x_test = x_test.astype("float32") / 255.0
x_train = np.expand_dims(x_train, -1)
x_test = np.expand_dims(x_test, -1)



print('x_train_shape:',x_train.shape)
print('no of images in x train',x_train.shape[0])
print('no of images in x test',x_test.shape[0])



model = keras.Sequential(
    [
        tf.keras.Input(shape=(28, 28, 1)),
        layers.Conv2D(32, kernel_size=(3, 3), activation="relu"),
        layers.MaxPooling2D(pool_size=(2, 2)),
        layers.Conv2D(64, kernel_size=(3, 3), activation="relu"),
        layers.MaxPooling2D(pool_size=(2, 2)),
        layers.Flatten(),
        layers.Dense(128, activation=tf.nn.relu),
        layers.Dense(28, activation=tf.nn.relu),
        layers.Dense(10, activation="softmax")
    ]
)
model.summary()
model.compile(loss="sparse_categorical_crossentropy", optimizer="adam", metrics=["accuracy"])

model.fit(x_train, y_train, batch_size=1000, epochs=8)


test_loss, test_acc = model.evaluate(x_test, y_test)
print("Test accuracy:", test_acc)

predictions = model.predict(x_test)

rand = random.randint(0,9974)
print(rand)
print(x_test.size)
plt.figure(figsize=(10, 10))
for i in range(25):
    plt.subplot(5, 5, i + 1)
    plt.imshow(x_test[rand, :, :, 0], cmap="gray")
    predicted_label = np.argmax(predictions[rand])
    true_label = y_test[rand]
    if(predicted_label != true_label):
        mis_color = "red"
    else: mis_color = "black"
    plt.title("Pred: {} | True: {}".format(predicted_label, true_label), color = mis_color)
    plt.axis("off")
    rand += 1
plt.show()

def Recognise(image_arr):
    prediction = model.predict(image_arr)
    predicted_label = np.argmax(prediction)
    print("Predicted label:", predicted_label)

    plt.figure(figsize=(6,3))
    plt.subplot(1,2,1)
    plt.imshow(image_arr[0], cmap=plt.cm.binary)
    plt.subplot(1,2,2)
    plot_value_array(prediction[0])
    plt.show()


def plot_value_array(predictions_array):
  plt.grid(False)
  plt.xticks(range(10))
  plt.yticks([])
  thisplot = plt.bar(range(10), predictions_array, color='lightblue')
  plt.ylim([0, 1])
  predicted_label = np.argmax(predictions_array)

  thisplot[predicted_label].set_color('lime')

