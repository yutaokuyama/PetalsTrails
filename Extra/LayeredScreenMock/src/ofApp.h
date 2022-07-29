#pragma once

#include "ofMain.h"
#include "ofxOscMessageEx.h"

#include "ofxOscPublisher.h"

#include "ofxGui.h"


class Screen {
public:
	Screen(int screenID, glm::vec2 position, glm::vec2 size) :position(position), size(size), id(screenID) {

	}
	void update() {
		luminance += (0.0 - luminance) / 12.;
	}

	void draw() {
		ofSetColor(255);
		ofNoFill();
		ofDrawRectangle(position, size.x, size.y);
		ofFill();

		ofSetColor(luminance * 255);
		ofDrawRectangle(position, size.x, size.y);
		ofSetColor(255);
		ofDrawBitmapString(ofToString(id), position + glm::vec2(0.0, 10.0));

	}

	void onHumanStandInFront() {
		luminance = 1.0;
	}
	int getScreenId() {
		return id;
	}
private:
	float luminance = 1.0;
	const glm::vec2 size;
	const glm::vec2 position;
	const int id;
};

class ofApp : public ofBaseApp {

public:
	void setup();
	void update();
	void draw();

	void mousePressed(int x, int y, int button);

	void drawGrid();
private:

	ofxPanel gui;
	ofxToggle isEnable;
	ofxToggle isAutoPlay;

	std::vector<int> sendPorts;
	void setupGui() {
		gui.setup();
		gui.add(isEnable.setup("isEnabled", true));
		gui.add(isAutoPlay.setup("AutoPlay", true));
	}

	void drawGui() {
		gui.draw();
	}
	int currentSelectedScreenId = 0;
	std::vector<Screen> screens;
	static const int NUM_ROW = 8;
	static const int NUM_COL = 12;


	void initScreens();
	void updateScreens() {
		for (auto& screen : screens) {
			screen.update();
		}
	}
	void drawScreens() {
		for (auto& screen : screens) {
			screen.draw();
		}
	}

	int convertRowAndColIdToScreenId(int row, int col) {
		return  96 - NUM_COL - row * NUM_COL + col;

	}

	int getScreenIdByMousePosition() {
		const int rowWidth = ofGetHeight() / NUM_ROW;
		const int colWidth = ofGetWidth() / NUM_COL;
		


		int colId = mouseX / colWidth;
		int rowId = mouseY / rowWidth;
		return   rowId * NUM_COL + colId;

		
	}

	void ofApp::autoPlay() {
		const int rowWidth = ofGetHeight() / NUM_ROW;
		const int colWidth = ofGetWidth() / NUM_COL;
		for (int i = 0; i < NUM_ROW; i++) {
			int rowId = i;
			const float x = abs(sin(ofGetElapsedTimef() / 10.0 + i * 1.75)) * ofGetWidth() * 0.95;
			int colId = x / colWidth;
			int screenArrayID = rowId * NUM_COL + colId;
			screens[screenArrayID].onHumanStandInFront();
			currentSelectedScreenId = screens[screenArrayID].getScreenId();;
			for (auto& port : sendPorts) {
				ofxSendOsc("localhost", port, "/point/piece", "C-" + ofToString(currentSelectedScreenId));

			}
		}
	}

};
