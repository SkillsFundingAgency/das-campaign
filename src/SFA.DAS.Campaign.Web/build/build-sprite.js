// Combines wwwroot/images/svg/*.svg into a single <symbol> sprite at wwwroot/images/sprite.svg.
// Each icon becomes a symbol whose id is its file name, referenced as:
//   <svg class="fiu-icon"><use href="/images/sprite.svg#mail"></use></svg>
const fs = require('fs');
const path = require('path');

const sourceDirectory = path.join(__dirname, '..', 'wwwroot', 'images', 'svg');
const outputFile = path.join(__dirname, '..', 'wwwroot', 'images', 'sprite.svg');

// Colour is left to CSS, so the hard coded fill the icon set ships with is dropped.
const rootAttributesToDrop = /\s(?:width|height|fill|xmlns|xmlns:xlink|version|class|id|style|aria-hidden|role)\s*=\s*"[^"]*"/gi;

function toSymbol(file) {
    const contents = fs.readFileSync(path.join(sourceDirectory, file), 'utf8').trim();
    const openingTag = contents.match(/<svg\b[^>]*>/i);

    if (!openingTag) {
        throw new Error(`${file} does not contain an <svg> element.`);
    }

    const id = path.basename(file, '.svg');
    const attributes = openingTag[0]
        .replace(/^<svg/i, '')
        .replace(/\/?>$/, '')
        .replace(rootAttributesToDrop, '')
        .trim();

    const body = contents
        .slice(openingTag.index + openingTag[0].length)
        .replace(/<\/svg>\s*$/i, '')
        .replace(/\sfill="(?!none")[^"]*"/gi, '')
        .trim();

    return `  <symbol id="${id}"${attributes ? ' ' + attributes : ''}>${body}</symbol>`;
}

const icons = fs.readdirSync(sourceDirectory)
    .filter(file => path.extname(file).toLowerCase() === '.svg')
    .sort();

if (icons.length === 0) {
    throw new Error(`No SVGs found in ${sourceDirectory}.`);
}

const sprite = [
    '<svg xmlns="http://www.w3.org/2000/svg" style="display:none" aria-hidden="true">',
    ...icons.map(toSymbol),
    '</svg>',
    ''
].join('\n');

fs.writeFileSync(outputFile, sprite);

console.log(`Wrote ${icons.length} icons to ${path.relative(process.cwd(), outputFile)}`);
