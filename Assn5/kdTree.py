import numpy as np
import matplotlib.pyplot as plt

class Node:
    def __init__(self, point, axis, left=None, right=None):
        self.point = point
        self.axis = axis
        self.left = left
        self.right = right

def build_kdtree(points, depth=0):
    if not points:
        return None

    k = len(points[0])  # Assuming all points have the same dimensionality
    axis = depth % k

    points.sort(key=lambda x: x[axis])
    median = len(points) // 2

    return Node(
        point=points[median],
        axis=axis,
        left=build_kdtree(points[:median], depth + 1),
        right=build_kdtree(points[median + 1:], depth + 1)
    )

def distance_squared(point1, point2):
    return np.sum((np.array(point1) - np.array(point2))**2)

# def closest_point_brute_force(point, points):
#     return min(points, key=lambda x: distance_squared(point, x))

def closest_point(node, point, depth=0, best=None):
    if node is None:
        return best

    k = len(point)
    axis = depth % k
    next_best = None
    next_branch = None

    if best is None or distance_squared(point, node.point) < distance_squared(point, best):
        next_best = node.point
    else:
        next_best = best

    if point[axis] < node.point[axis]:
        next_branch = node.left
    else:
        next_branch = node.right

    return closest_point(next_branch, point, depth + 1, next_best)

def plot_kdtree(tree, ax, mins, maxs):
    if tree is not None:
        axis = tree.axis
        point = tree.point

        # Plot vertical or horizontal line based on the axis
        if axis == 0:
            ax.plot([point[0], point[0]], [mins[1], maxs[1]], color='gray', linestyle='-', linewidth=0.5)
        else:
            ax.plot([mins[0], maxs[0]], [point[1], point[1]], color='gray', linestyle='-', linewidth=0.5)

        # Recursively plot left and right subtrees
        plot_kdtree(tree.left, ax, mins, point)
        plot_kdtree(tree.right, ax, point, maxs)

# Example usage:
points = [(2, 3), (5, 4), (9, 6), (4, 7), (8, 1), (7, 2)]
tree = build_kdtree(points)

query_point = (9, 2)
closest = closest_point(tree, query_point)

# Scatter plot of points
plt.figure(figsize=(8, 6))
plt.scatter(*zip(*points), color='blue', label='Points')
plt.scatter(*query_point, color='red', label='Query Point')
plt.scatter(*closest, color='green', label='Nearest Neighbor')

# Plot the KDTree
ax = plt.gca()
plot_kdtree(tree, ax, np.min(points, axis=0), np.max(points, axis=0))

plt.title('KDTree with Nearest Neighbor Search')
plt.xlabel('X')
plt.ylabel('Y')
plt.legend()
plt.grid(True)
plt.show()
