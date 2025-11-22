cmake_minimum_required(VERSION 3.10)
set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

project(hello VERSION 1.0)

find_package(SFML 2 REQUIRED network audio graphics window system)
add_subdirectory(blah)

add_executable(hello main.cpp)
target_include_directories(hello PUBLIC ${SFML_INCLUDE_DIR})
target_link_libraries(hello PUBLIC blah ${SFML_LIBRARIES} ${SFML_DEPENDENCIES})
