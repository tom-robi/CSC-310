import numpy as np
from scipy.optimize import curve_fit

# Read data from CSV file
with open('insertion_times.csv', 'r') as file:
    lines = file.readlines()[1:] # skip header line

# Extract x and y values from the file
x_values = []
y_values = []
for line in lines:
    values = line.strip().split(',')
    x_values.append(float(values[0]))
    y_values.append(float(values[1]))

# Define the log function
def log_func(x, a, b, c):
    return a * np.log2(x) + c

# Initial guess for parameters
initial_guess = [1, 2, 1]

# Fit the curve to the data
params, covariance = curve_fit(log_func, x_values, y_values, p0=initial_guess)

# Extract the fitted parameters
a, b, c = params

print("Fitted parameters:")
print("a:", a)
print("b:", b)
print("c:", c)
