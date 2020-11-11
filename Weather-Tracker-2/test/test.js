/*
 * add how to install it and how to run it
 * npm install -g testcafe
 * testcafe edge test.js
 * testcafe edge test.js -t "Weather Fetch Test"
*/

import { Selector } from 'testcafe'

fixture`Base Test Set`
    .page`https://localhost:44367/Home/Weather`;

// Select the root org
test('Weather Fetch Test', async t => {
    await t
        // Get the list of orgs from the root
        .typeText('#txtCity', 'Snoqualmie')
        .click('#btnSubmit')
        //.debug()
        .expect(Selector('#lblCity').innerText).eql('Snoqualmie')
});

test('Weather History Test', async t => {
    await t
        // Get the list of orgs from the root
        .typeText('#txtCity', 'Laramie')
        .click('#btnSubmit')
        .expect(Selector('#lblCity').innerText).eql('Laramie')
        .click('#txtCity')
        .pressKey('ctrl+a delete')
        .typeText('#txtCity', 'Snoqualmie')
        .click('#btnSubmit')
        .expect(Selector('#lblCity').innerText).eql('Snoqualmie')
        .click('#txtCity')
        .pressKey('ctrl+a delete')
        .typeText('#txtCity', 'Laramie')
        .click('#btnFetchHistory')
        .expect(Selector('#lblCity').innerText).eql('Laramie')
});
