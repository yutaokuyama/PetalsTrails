#include "ofApp.h"

//--------------------------------------------------------------
void ofApp::initScreens() {
	const int rowWidth = ofGetHeight() / NUM_ROW;
	const int colWidth = ofGetWidth() / NUM_COL;

	for (int row = 0; row < NUM_ROW; row++) {
		const int yPosition = row * rowWidth;
		for (int col = 0; col < NUM_COL; col++) {
			const int xPosition = col * colWidth;
			const int screenID = (96 - 12) - row * NUM_COL + col;
			screens.push_back(Screen(screenID, glm::vec2(xPosition, yPosition), glm::vec2(colWidth,rowWidth)));
		}
	}
}

void ofApp::setup() {
	ofBackground(0);
	initScreens();
	//ofxPublishOsc("localhost", 6666, "/point/piece", currentSelectedScreenId);
	setupGui();

	sendPorts.push_back(6666);
	//sendPorts.push_back(6667);
	//sendPorts.push_back(6668);
	//sendPorts.push_back(6669);

}

//--------------------------------------------------------------
void ofApp::update() {
	if (isEnable) {
		if (isAutoPlay) {
			autoPlay();
		}
		else {
			int screenArrayID = getScreenIdByMousePosition();
			screens[screenArrayID].onHumanStandInFront();
			currentSelectedScreenId = screens[screenArrayID].getScreenId();
			for (auto& port : sendPorts) {
				ofxSendOsc("localhost", port, "/point/piece", "C-" + ofToString(currentSelectedScreenId));

			}
		}
	}

	updateScreens();
}

//--------------------------------------------------------------
void ofApp::draw() {
	drawScreens();
	drawGui();
}

void ofApp::mousePressed(int x, int y, int button) {
	isEnable = !isEnable;
}
